using Mikita.FileSystems.Files;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Folders;

public static class FilePicking
	{
		extension(IFolder folder)
			{
				public IAsyncEnumerable<IFile> Files
					=> folder.Entries
						.Select(x => x.AsFile)
						.Where(async (x, cancel) => await x.Exists(cancel));

				public Task<bool> ContainsFileWithName
					(
						string name,
						CancellationToken cancel = default
					)
					=> folder
						.FileWithName(name)
						.Exists(cancel);

				public IFile FileWithName
					(
						string name
					)
					=> folder
						.EntryWithName(name)
						.AsFile;
			}
	}