using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public sealed class NonStopTaskPool
	(
		ICollection<ITaskPool> deadPools,
		ICollection<ITaskPool> freePools,
		IRef<ITaskPool?> lastPool,
		Func<ITaskPool> newPool
	)
	: ITaskPool
	{
		public void Put(CancellableTask task)
			{
				if (lastPool.Value is null)
					lastPool.SetTo(PickNextPool());
				lastPool.Value!.Put(task);
			}

		public Task Stop()
			{
				lastPool.TryPullOut(deadPools.Add);

				var poolsToStop = deadPools.ToArray();
				deadPools.Clear();

				return Parallel.ForEachAsync(poolsToStop, Stop);
			}

		private async ValueTask Stop
			(
				ITaskPool pool,
				CancellationToken token
			)
			{
				await pool.Stop();
				freePools.Add(pool);
			}

		private ITaskPool PickNextPool()
			=> PickFreePool() ?? newPool();

		private ITaskPool? PickFreePool()
			{
				var pool = freePools.LastOrDefault();
				if (pool is not null) freePools.Remove(pool);
				return pool;
			}
	}