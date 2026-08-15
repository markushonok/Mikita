using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Folders;
using Mikita.IO.Paths;
using Mikita.IO.Paths.Formats;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.FileSystems.Native;

public sealed partial class NativeFolder
	{
		public static IFolder At(IPath path)
			=> new NativeFolder(path);
	}

public sealed partial class NativeFolder
	(
		IPath path
	)
	: IFolder
	{
		IReadOnlyFile IReadOnlyFolder.FileAt(IPath subpath)
			=> FileAt(subpath);

		public IFile FileAt(IPath subpath)
			=> NativeFile.At(path / subpath);

		IReadOnlyFolder IReadOnlyFolder.SubFolderAt(IPath subpath)
			=> SubFolderAt(subpath);

		public IFolder SubFolderAt(IPath subpath)
			=> At(path / subpath);

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