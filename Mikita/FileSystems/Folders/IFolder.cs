using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;
using System.Collections.Generic;

namespace Mikita.FileSystems.Folders;

public interface IFolder: IReadOnlyFolder, IEntry
	{
		new IFile FileAt(IPath path);

		new IFolder SubFolderAt(IPath path);

		new IAsyncEnumerable<IFoundEntry> Entries { get; }
	}