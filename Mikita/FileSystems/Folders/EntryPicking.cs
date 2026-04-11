using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Paths;

namespace Mikita.FileSystems.Folders;

public static class EntryPicking
	{
		extension(IFolder folder)
			{
				public IUnspecifiedEntry EntryWithName
					(
						string name
					)
					=> folder.EntryAt(Path.From(name));
			}
	}