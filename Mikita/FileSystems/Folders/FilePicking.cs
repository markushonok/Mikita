using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Folders;

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