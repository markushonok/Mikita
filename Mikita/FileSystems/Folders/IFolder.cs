using Mikita.FileSystems.Entries;
using System.Collections.Generic;

namespace Mikita.FileSystems.Folders;

public interface IFolder: ILocatedEntry
	{
		IUnspecifiedEntry EntryWithName(string name);

		IAsyncEnumerable<IUnspecifiedEntry> Entries { get; }
	}