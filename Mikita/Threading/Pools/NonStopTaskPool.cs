using Mikita.Logging;
using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public sealed partial class NonStopTaskPool
	{
		/// <summary>
		/// Creates a non-thread-safe rotating pool backed by reusable
		/// <see cref="TaskPool"/> instances.
		/// </summary>
		public static ITaskPool NewWith
			(
				ILog log
			)
			=> new NonStopTaskPool
				(
					deadPools: [],
					freePools: [],
					lastPool: Ref<ITaskPool>.Null,
					newPool: () => TaskPool.NewWith(log),
					freeAccess: new Lock()
				);
	}

/// <summary>
/// Provides a task group that can be replaced while its predecessor stops.
/// </summary>
/// <remarks>
/// Public API calls must not execute concurrently. Child-stop continuations may
/// finish concurrently: access to reusable children is synchronized internally.
/// A child is reused only after a successful stop.
/// </remarks>
/// <param name="deadPools">
/// Children detached and awaiting a stop request.
/// </param>
/// <param name="freePools">
/// Successfully stopped children available for reuse.
/// </param>
/// <param name="lastPool">
/// Child receiving new tasks, or <see langword="null"/>.
/// </param>
/// <param name="newPool">Creates a child when none is reusable.</param>
/// <param name="freeAccess">
/// Serializes internal access to <paramref name="freePools"/>.
/// </param>
public sealed partial class NonStopTaskPool
	(
		ICollection<ITaskPool> deadPools,
		ICollection<ITaskPool> freePools,
		IRef<ITaskPool?> lastPool,
		Func<ITaskPool> newPool,
		Lock freeAccess
	)
	: ITaskPool
	{
		/// <summary>
		/// Starts a task in the active child, reusing or creating one if needed.
		/// </summary>
		public void Put(CancellableTask task)
			{
				if (lastPool.Value is null)
					lastPool.SetTo(PickNextPool());
				lastPool.Value!.Put(task);
			}

		/// <summary>
		/// Detaches the active child and stops the pending detached snapshot. A
		/// subsequent <see cref="Put"/> may immediately select another child.
		/// </summary>
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
				lock (freeAccess) freePools.Add(pool);
			}

		private ITaskPool PickNextPool()
			=> PickFreePool() ?? newPool();

		private ITaskPool? PickFreePool()
			{
				lock (freeAccess)
					{
						var pool = freePools.LastOrDefault();
						if (pool is not null) freePools.Remove(pool);
						return pool;
					}
			}
	}