using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Paths;
using System.Collections.Generic;

namespace Mikita.IO.Folders;

public interface IReadOnlyFolder: IReadOnlyEntry
	{
		IReadOnlyFile FileAt(IPath path);
		
		IReadOnlyFolder SubFolderAt(IPath path);
		
		IAsyncEnumerable<IFoundReadOnlyEntry> Entries { get; }
	}