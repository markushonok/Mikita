using Mikita.Routines;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Executes accepted tasks one at a time through another task pool.
/// </summary>
/// <remarks>
/// The source owns cancellation, failure handling, and stopping. Waiting work
/// observes the source's cancellation token before starting. Public API access
/// follows the source's contract; use WithSerialAccess around this decorator
/// when Put and Stop may overlap. Do not access the source directly.
/// </remarks>
/// <param name="source">Owns accepted tasks and their stopping group.</param>
/// <param name="execution">
/// A semaphore with initial and maximum counts of one, used only by this
/// decorator to serialize task execution.
/// </param>
public sealed class SequentialTaskPool
	(
		ITaskPool source,
		SemaphoreSlim execution
	)
	: ITaskPool
	{
		public void Put(CancellableTask task)
			=> source.Put
				(
					async cancel =>
						{
							await execution.WaitAsync(cancel);
							try
								{
									cancel.ThrowIfCancellationRequested();
									await task(cancel);
								}
							finally
								{
									execution.Release();
								}
						}
				);

		public Task Stop()
			=> source.Stop();
	}