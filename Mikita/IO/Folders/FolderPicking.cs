using Mikita.IO.Entries;
using Mikita.IO.Paths;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.IO.Folders;

public static class FolderPicking
	{
		extension(IReadOnlyFolder folder)
			{
				public IAsyncEnumerable<IReadOnlyFolder> SubFolders
					=> folder.Entries
						.Select(x => x.AsFolder)
						.WhereNotNull();

				public IReadOnlyFolder SubFolderWithName
					(
						string name
					)
					=> folder.SubFolderAt(SingleElementPath.From(name));
			}

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