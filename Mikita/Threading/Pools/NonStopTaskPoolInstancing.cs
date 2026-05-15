using Mikita.Logging;
using Mikita.Structs.Referring;

namespace Mikita.Threading.Pools;

public static class NonStopTaskPoolInstancing
	{
		extension(NonStopTaskPool)
			{
				public static ITaskPool NewWith
					(
						ILog log
					)
					=> new NonStopTaskPool
						(
							deadPools: [],
							freePools: [],
							lastPool: Ref<ITaskPool>.Null,
							newPool: () => TaskPool.NewWith(log)
						);
			}
	}