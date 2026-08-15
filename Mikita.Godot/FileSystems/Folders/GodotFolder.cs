using Godot;
using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using Mikita.Godot.FileSystems.Files;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Godot.FileSystems.Folders;

public sealed class GodotFolder
	(
		IPath path,
		string scheme
	)
	: IFolder
	{
		IReadOnlyFile IReadOnlyFolder.FileAt(IPath subpath)
			=> FileAt(subpath);

		public IFile FileAt(IPath subpath)
			=> new GodotFile(path / subpath, scheme);

		IReadOnlyFolder IReadOnlyFolder.SubFolderAt(IPath subpath)
			=> SubFolderAt(subpath);

		public IFolder SubFolderAt(IPath subpath)
			=> new GodotFolder(path / subpath, scheme);

		IAsyncEnumerable<IFoundReadOnlyEntry> IReadOnlyFolder.Entries
			=> Entries;

		public IAsyncEnumerable<IFoundEntry> Entries
			=> this.EntriesWith(EntryInfos());

		public Task Create
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DirAccess.MakeDirRecursiveAbsolute(SchemePathString),
					cancel
				);

		public Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DirAccess.RenameAbsolute(SchemePathString, ToSchemePath(destination, scheme)),
					cancel
				);

		public Task Delete
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DeleteRecursive(SchemePathString),
					cancel
				);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DirAccess.DirExistsAbsolute(SchemePathString),
					cancel
				);

		public IPath Path
			=> path;

		private string SchemePathString
			=> ToSchemePath(path, scheme);

		private static string ToSchemePath
			(
				IPath path,
				string scheme
			)
			{
				var relative = path.ToDefaultString().TrimStart('/');
				var normalizedScheme = scheme.EndsWith("://")
					? scheme
					: $"{scheme}://";

				return relative is ""
					? normalizedScheme
					: $"{normalizedScheme}{relative}";
			}

		private IEnumerable<FoundEntryInfo> EntryInfos()
			{
				using var dir = DirAccess.Open(SchemePathString);
				if (dir is null)
					yield break;

				dir.ListDirBegin();
				try
					{
						while (true)
							{
								var name = dir.GetNext();
								if (string.IsNullOrEmpty(name))
									break;

								if (name is "." or "..")
									continue;

								yield return new FoundEntryInfo
									(
										name,
										dir.CurrentIsDir()
											? EntryType.Folder
											: EntryType.File
									);
							}
					}
				finally
					{
						dir.ListDirEnd();
					}
			}

		private static void DeleteRecursive(string folderPath)
			{
				using var dir = DirAccess.Open(folderPath);
				if (dir == null) return;

				dir.IncludeHidden = true;

				foreach (var name in dir.GetDirectories())
					DeleteRecursive(folderPath.PathJoin(name));

				foreach (var name in dir.GetFiles())
					DirAccess.RemoveAbsolute(folderPath.PathJoin(name));

				DirAccess.RemoveAbsolute(folderPath);
			}
	}
