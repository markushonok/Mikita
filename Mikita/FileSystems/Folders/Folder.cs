using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Threading;
using System.Threading.Tasks;
using File = Mikita.FileSystems.Files.File;

namespace Mikita.FileSystems.Folders;

public sealed class Folder
	(
		IPath path
	)
	: IFolder
	{
		IReadOnlyFile IReadOnlyFolder.FileAt(IPath subpath)
			=> FileAt(subpath);

		public IFile FileAt(IPath subpath)
			=> new File(path / subpath);

		IReadOnlyFolder IReadOnlyFolder.SubFolderAt(IPath subpath)
			=> SubFolderAt(subpath);

		public IFolder SubFolderAt(IPath subpath)
			=> new Folder(path / subpath);

		IAsyncEnumerable<IFoundReadOnlyEntry> IReadOnlyFolder.Entries
			=> Entries;

		public IAsyncEnumerable<IFoundEntry> Entries
			=> this.EntriesWith(EnumerateEntries());

		private IEnumerable<FoundEntryInfo> EnumerateEntries()
			=> new FileSystemEnumerable<FoundEntryInfo>
				(
					PathString,
					FoundEntryInfo.From,
					EnumerationOptions
				);

		private static readonly EnumerationOptions EnumerationOptions
			= new()
				{
					RecurseSubdirectories = false,
					AttributesToSkip = FileAttributes.None,
					IgnoreInaccessible = false
				};

		public Task Create
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.CreateDirectory(PathString),
					cancel
				);

		public Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Move(PathString, destination.ToDefaultString()),
					cancel
				);

		public Task Delete
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Delete(PathString, recursive: true),
					cancel
				);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Exists(PathString),
					cancel
				);

		public IPath Path
			=> path;

		private string PathString
			=> path.ToDefaultString();
	}