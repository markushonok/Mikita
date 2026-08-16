using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Folders;
using Mikita.IO.Paths;
using Mikita.IO.Paths.Formats;
using Mikita.Structs.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TPath = Mikita.IO.Paths.Path;

namespace Mikita.IO.FileSystems.Embedded;

public sealed partial class EmbeddedFolder
	{
		public static IReadOnlyFolder From
			(
				Assembly assembly
			)
			{
				var files = new Dictionary<IPath, IReadOnlyFile>();
				var folderPaths = new HashSet<IPath> {TPath.Current};

				AddResources(assembly, files, folderPaths);

				var entries = EntriesFrom(files.Keys, folderPaths);
				var folders = FoldersFrom(files, folderPaths, entries);

				return folders[TPath.Current];
			}

		public static IReadOnlyFolder With
			(
				IReadOnlyDictionary<IPath, IReadOnlyFile> files,
				IReadOnlyDictionary<IPath, IReadOnlyFolder> folders,
				ILookup<IPath, FoundEntryInfo> entries,
				IPath path
			)
			{
				var fileMap = Map.Of<IPath, IReadOnlyFile>
					(
						x => files.TryGetValue(x, out var file)
							? file
							: NonExistentFile.At(x)
					);

				var folderMap = Map.Of<IPath, IReadOnlyFolder>
					(
						x => folders.TryGetValue(x, out var folder)
							? folder
							: NonExistentFolder.At(x)
					);

				return new EmbeddedFolder(fileMap, folderMap, entries, path);
			}

		private static void AddResources
			(
				Assembly assembly,
				IDictionary<IPath, IReadOnlyFile> files,
				ISet<IPath> folderPaths
			)
			{
				foreach (var resource in assembly.GetManifestResourceNames())
					AddResource(assembly, resource, files, folderPaths);
			}

		private static void AddResource
			(
				Assembly assembly,
				string resource,
				IDictionary<IPath, IReadOnlyFile> files,
				ISet<IPath> folderPaths
			)
			{
				var path = ResourcePathFrom(resource);

				if (path.Elements is [])
					throw EmptyPathException;

				if (folderPaths.Contains(path))
					throw ConflictingEntryException(path);

				files.Add(path, EmbeddedFile.At(assembly, path));

				AddFolderPaths(path, files, folderPaths);
			}

		private static void AddFolderPaths
			(
				IPath path,
				IDictionary<IPath, IReadOnlyFile> files,
				ISet<IPath> folderPaths
			)
			{
				foreach (var folderPath in path.Ancestors)
					{
						if (files.ContainsKey(folderPath))
							throw ConflictingEntryException(folderPath);

						folderPaths.Add(folderPath);
					}
			}

		private static ILookup<IPath, FoundEntryInfo> EntriesFrom
			(
				IEnumerable<IPath> filePaths,
				IEnumerable<IPath> folderPaths
			)
			=> ChildrenFrom(filePaths, EntryType.File)
				.Concat(ChildrenFrom(folderPaths, EntryType.Folder))
				.ToLookup(x => x.Parent, x => x.Info);

		private static IReadOnlyDictionary<IPath, IReadOnlyFolder> FoldersFrom
			(
				IReadOnlyDictionary<IPath, IReadOnlyFile> files,
				IEnumerable<IPath> folderPaths,
				ILookup<IPath, FoundEntryInfo> entries
			)
			{
				var folders = new Dictionary<IPath, IReadOnlyFolder>();

				foreach (var path in folderPaths)
					{
						var folder = With
							(
								files,
								folders,
								entries,
								path
							);
						folders.Add(path, folder);
					}

				return folders;
			}

		private static IPath ResourcePathFrom(string resource)
			=> TPath.From(resource, PathFormat.Unix);

		private static IEnumerable<ChildEntryInfo> ChildrenFrom
			(
				IEnumerable<IPath> paths,
				EntryType type
			)
			=> paths
				.Where(path => path.Elements is not [])
				.Select
					(
						path => new ChildEntryInfo
							(
								path.Parent,
								new FoundEntryInfo(path.Elements[^1], type)
							)
					);

		private static Exception EmptyPathException
			=> new InvalidOperationException
				(
					"An embedded resource must have"
					+ " a non-empty logical path."
				);

		private static Exception ConflictingEntryException
			(
				IPath path
			)
			=> new InvalidOperationException
				(
					$"The embedded resource entry at"
					+ $" '{path.ToDefaultString()}'"
					+ $" is both a file and a folder."
				);
	}

public sealed partial class EmbeddedFolder
	(
		IReadOnlyMap<IPath, IReadOnlyFile> files,
		IReadOnlyMap<IPath, IReadOnlyFolder> folders,
		ILookup<IPath, FoundEntryInfo> entries,
		IPath path
	)
	: IReadOnlyFolder
	{
		public IReadOnlyFile FileAt(IPath subpath)
			=> files[path / subpath];

		public IReadOnlyFolder SubFolderAt(IPath subpath)
			=> folders[path / subpath];

		public IAsyncEnumerable<IFoundReadOnlyEntry> Entries
			=> this.EntriesWith(entries[path]);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.FromResult(folders[path] is not NonExistentFolder);

		public IPath Path
			=> path;
	}