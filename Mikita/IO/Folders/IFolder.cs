using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Paths;
using System.Collections.Generic;

namespace Mikita.IO.Folders;

public interface IFolder: IReadOnlyFolder, IEntry
	{
		new IFile FileAt(IPath path);

		new IFolder SubFolderAt(IPath path);

		new IAsyncEnumerable<IFoundEntry> Entries { get; }
	}