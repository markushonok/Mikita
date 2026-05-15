using Mikita.Logging;
using Mikita.Structs.Referring;
using System.Threading;

namespace Mikita.Threading.Pools;

public static class TaskPoolInstancing
	{
		extension(TaskPool)
			{
				public static ITaskPool NewWith
					(
						ILog log
					)
					=> new TaskPool
						(
							tasks: [],
							cancelSource: Ref<CancellationTokenSource>.Null,
							log
						);
			}
	}