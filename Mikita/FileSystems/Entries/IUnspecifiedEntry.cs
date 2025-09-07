using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;

namespace Mikita.FileSystems.Entries;

public interface IUnspecifiedEntry
	{
		IFile AsFile { get; }

		IFolder AsFolder { get; }
	}