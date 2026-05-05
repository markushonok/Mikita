using Mikita.Logging;
using Mikita.Structs.Referring;

namespace Mikita.Threading.Pools;

public static class ReusableTaskPoolInstancing
	{
		extension(ReusableTaskPool)
			{
				public static ITaskPool NewWith
					(
						ILog log
					)
					=> new ReusableTaskPool
						(
							deadPools: [],
							lastPool: Ref<ITaskPool>.Null,
							nextPool: () => InstantTaskPool.NewWith(log)
						);
			}
	}