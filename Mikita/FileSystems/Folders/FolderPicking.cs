using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Paths;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Folders;

public static class FolderPicking
	{
		extension(IFolder folder)
			{
				public IAsyncEnumerable<IFolder> SubFolders
					=> folder.Entries
						.Select(x => x.AsFolder)
						.WhereNotNull();

				public IFolder SubFolderWithName
					(
						string name
					)
					=> folder.SubFolderAt(SingleElementPath.From(name));
			}
	}