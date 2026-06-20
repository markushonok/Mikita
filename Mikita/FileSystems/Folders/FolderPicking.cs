using Mikita.FileSystems.Paths;
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
					=> folder.Entries
						.Select(x => x.AsFolder)
						.Where(async (x, cancel) => await x.Exists(cancel));

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

				public IFolder SubFolderAt
					(
						IPath path
					)
					=> folder.EntryAt(path).AsFolder;
			}
	}