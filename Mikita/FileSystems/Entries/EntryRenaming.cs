using Mikita.FileSystems.Paths;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Entries;

public static class EntryRenaming
	{
		extension(ILocatedEntry entry)
			{
				public Task RenameTo
					(
						string newName,
						CancellationToken cancel = default
					)
					=> entry.MoveTo
						(
							entry.Path.Parent / newName,
							cancel
						);
			}
	}
