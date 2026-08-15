using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;
using System.Collections.Generic;

namespace Mikita.FileSystems.Folders;

public interface IReadOnlyFolder: IReadOnlyEntry
	{
		IReadOnlyFile FileAt(IPath path);
		
		IReadOnlyFolder SubFolderAt(IPath path);
		
		IAsyncEnumerable<IFoundReadOnlyEntry> Entries { get; }
	}