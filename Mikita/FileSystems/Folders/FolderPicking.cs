using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Folders;

public static class FolderPicking
	{
		extension(IFolder folder)
			{
				public IAsyncEnumerable<IFolder> SubFolders
					(
						CancellationToken cancel
					)
					=> folder.Entries
						.Select(x => x.AsFolder)
						.WhereAwait(async x => await x.Exists(cancel));

				public Task<bool> ContainsSubFolderWithName
					(
						string name,
						CancellationToken cancel
					)
					=> folder
						.SubFolderWithName(name)
						.Exists(cancel);

				public IFolder SubFolderWithName
					(
						string name
					)
					=> folder
						.EntryWithName(name)
						.AsFolder;
			}
	}