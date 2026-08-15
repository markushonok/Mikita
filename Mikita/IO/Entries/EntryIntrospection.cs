using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Entries;

public static class EntryIntrospection
	{
		extension(IEntry entry)
			{
				public async Task<bool> NotExists
					(
						CancellationToken cancel = default
					)
					=> !(await entry.Exists(cancel));

				public string Name
					=> entry.Path.Elements[^1];
			}
	}