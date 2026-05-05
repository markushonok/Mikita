using Mikita.Logging;
using System.Threading;

namespace Mikita.Threading.Pools;

public static class InstantTaskPoolInstancing
	{
		extension(InstantTaskPool)
			{
				public static ITaskPool NewWith
					(
						ILog log
					)
					=> new InstantTaskPool
						(
							tasks: [],
							new CancellationTokenSource(),
							log
						);
			}
	}