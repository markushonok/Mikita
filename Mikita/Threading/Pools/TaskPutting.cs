using System;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>Provides task-pool conveniences for non-cancellable work.</summary>
public static class TaskPutting
	{
		extension(ITaskPool pool)
			{
				/// <summary>
				/// Puts a task that ignores the pool's cancellation token. A stop still
				/// waits for it to finish.
				/// </summary>
				public void Put(Func<Task> task)
					=> pool.Put(cancel => task());
			}
	}