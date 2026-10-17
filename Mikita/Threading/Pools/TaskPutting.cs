using System;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>Provides task-pool conveniences for non-cancellable work.</summary>
public static class TaskPutting
	{
		extension(ITaskPool pool)
			{
				/// <summary>
				/// Observes already-started work. The owner controls its
				/// cancellation; the pool only waits for it and may reject it.
				/// </summary>
				public void Put(Task task)
					=> pool.Put(() => task);

				/// <summary>
				/// Puts a task that ignores the pool's cancellation token. A stop still
				/// waits for it to finish.
				/// </summary>
				public void Put(Func<Task> task)
					=> pool.Put(cancel => task());
			}
	}