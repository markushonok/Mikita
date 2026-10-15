using Mikita.Logging;
using Mikita.Structs.Referring;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>Provides standard task-pool compositions.</summary>
public static class TaskPoolInstancing
	{
		extension(TaskPool)
			{
				/// <summary>
				/// Creates a pool safe for its own concurrent task completions, but not
				/// for concurrent calls to its public API.
				/// </summary>
				public static ITaskPool NewWith
					(
						ILog log
					)
					=> new TaskPool
						(
							tasks: [],
							cancelSource: Ref<CancellationTokenSource>.Null,
							log,
							tasksAccess: new Lock()
						);
			}

		extension(ITaskPool tasks)
			{
				/// <summary>
				/// Executes accepted tasks one at a time. The source must no longer
				/// be accessed directly. This does not add concurrent API access.
				/// </summary>
				public ITaskPool WithSequentialExecution
					=> new SequentialTaskPool
						(
							source: tasks,
							execution: new SemaphoreSlim(1, 1)
						);

				/// <summary>
				/// Adds serialized public access. The source must no longer be accessed
				/// directly.
				/// </summary>
				public ITaskPool WithSerialAccess
					=> new SerialAccessTaskPool
						(
							source: tasks,
							stopping: Ref<Task>.Null,
							access: new Lock()
						);

				/// <summary>
				/// Rejects work after the first stop. The source must no longer be
				/// accessed directly.
				/// </summary>
				public ITaskPool WithSingleUse
					=> new SingleUseTaskPool
						(
							source: tasks,
							released: Ref.To(false)
						);
			}
	}