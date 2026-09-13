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
			}
	}