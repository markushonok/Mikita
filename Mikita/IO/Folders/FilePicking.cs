using Mikita.IO.Entries;
using Mikita.IO.Files;
using Mikita.IO.Paths;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.IO.Folders;

public static class FilePicking
	{
		extension(IFolder folder)
			{
				public IAsyncEnumerable<IFile> Files
					=> folder
						.Entries
						.Select(x => x.AsFile)
						.WhereNotNull();

				public IFile FileWithName
					(
						string name
					)
					=> folder.FileAt(SingleElementPath.From(name));
			}
	}