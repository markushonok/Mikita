using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using Mikita.FileSystems.Paths;

namespace Mikita.FileSystems.Entries;

public sealed class Entry
	(
		IPath path
	)
	: IUnspecifiedEntry
	{
		public IFile AsFile
			=> new File(path);

		public IFolder AsFolder
			=> new Folder(path);
	}