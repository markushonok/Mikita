using Mikita.Logging;

namespace Mikita.Threading.Pools;

public static class SingleTaskPoolInstancing
	{
		extension(SingleTaskPool)
			{
				public static ITaskPool NewWith(ILog log)
					=> new SingleTaskPool
						(
							basePool: NonStopTaskPool.NewWith(log)
						);
			}
	}