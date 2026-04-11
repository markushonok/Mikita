using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Paths;
using System.Collections.Generic;

namespace Mikita.FileSystems.Folders;

public interface IFolder: ILocatedEntry
	{
		IUnspecifiedEntry EntryAt(IPath path);

		IAsyncEnumerable<IUnspecifiedEntry> Entries { get; }
	}