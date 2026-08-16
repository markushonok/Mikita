using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Paths;
using Mikita.IO.Paths.Formats;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Folders;

public sealed partial class NonExistentFolder
	{
		public static IReadOnlyFolder At(IPath path)
			=> new NonExistentFolder(path);
	}

public sealed partial class NonExistentFolder
	(
		IPath path
	)
	: IReadOnlyFolder
	{
		public IAsyncEnumerable<IFoundReadOnlyEntry> Entries
			=> throw NotFoundException;

		public IReadOnlyFile FileAt(IPath subpath)
			=> NonExistentFile.At(path / subpath);

		public IReadOnlyFolder SubFolderAt(IPath subpath)
			=> NonExistentFolder.At(path / subpath);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.FromResult(false);

		public IPath Path
			=> path;

		private DirectoryNotFoundException NotFoundException
			=> new
				(
					$"The folder at '{path.ToDefaultString()}' does not exist."
				);
	}