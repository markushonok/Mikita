using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Entries;

public static class EntryIntrospection
	{
		extension(IUnspecifiedEntry entry)
			{
				public Task<bool> IsFile
					(
						CancellationToken cancel = default
					)
					=> entry.AsFile.Exists(cancel);

				public Task<bool> IsFolder
					(
						CancellationToken cancel = default
					)
					=> entry.AsFolder.Exists(cancel);
			}
	}