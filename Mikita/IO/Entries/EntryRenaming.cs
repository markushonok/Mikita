using Mikita.IO.Paths;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Entries;

public static class EntryRenaming
	{
		extension(IEntry entry)
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
