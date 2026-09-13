using Mikita.Logging;
using Mikita.Routines;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public sealed partial class SingleTaskPool
	{
		/// <summary>
		/// Creates a non-thread-safe single-task pool whose previous task may
		/// finish while its replacement starts.
		/// </summary>
		public static ITaskPool NewWith(ILog log)
			=> new SingleTaskPool
				(
					source: NonStopTaskPool.NewWith(log)
				);
	}

/// <summary>
/// Replaces the current task whenever another task is put.
/// </summary>
/// <remarks>
/// <paramref name="source"/> must permit a new group immediately after
/// <see cref="ITaskPool.Stop"/> is called. Public API calls are not
/// thread-safe.
/// </remarks>
/// <param name="source">Pool providing replaceable task groups.</param>
public sealed partial class SingleTaskPool
	(
		ITaskPool source
	)
	: ITaskPool
	{
		/// <summary>
		/// Starts stopping the previous task without waiting, then puts the new
		/// task into the replacement group.
		/// </summary>
		public void Put(CancellableTask task)
			{
				source.Stop().Forget();
				source.Put(task);
			}

		/// <summary>Stops the current task.</summary>
		public Task Stop()
			=> source.Stop();
	}