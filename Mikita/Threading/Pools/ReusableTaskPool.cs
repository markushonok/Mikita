using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public sealed class ReusableTaskPool
	(
		ICollection<ITaskPool> deadPools,
		IRef<ITaskPool?> lastPool,
		Func<ITaskPool> nextPool
	)
	: ITaskPool
	{
		public void Put
			(
				CancellableTask task
			)
			{
				if (lastPool.Value is null)
					lastPool.SetTo(nextPool());

				lastPool.Value!.Put(task);
			}

		public Task Stop()
			{
				lastPool.TryPullOut(deadPools.Add);

				return Parallel.ForEachAsync
					(
						deadPools.ToArray(),
						Stop
					);
			}

		private async ValueTask Stop
			(
				ITaskPool pool,
				CancellationToken token
			)
			{
				await pool.Stop();
				deadPools.Remove(pool);
			}
	}