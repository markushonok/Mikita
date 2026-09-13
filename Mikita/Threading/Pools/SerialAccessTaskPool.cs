using Mikita.Routines;
using Mikita.Structs.Referring;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Serializes access to another <see cref="ITaskPool"/>.
/// </summary>
/// <remarks>
/// All access to <paramref name="source"/> must pass through this instance.
/// Concurrent stops share one operation. Puts made while it is incomplete are
/// discarded. The decorator does not serialize execution of accepted tasks.
/// </remarks>
/// <param name="source">Pool receiving serialized calls.</param>
/// <param name="stopping">Current incomplete stop, if any.</param>
/// <param name="access">Serializes public API calls and stop state.</param>
public sealed class SerialAccessTaskPool
	(
		ITaskPool source,
		IRef<Task?> stopping,
		Lock access
	)
	: ITaskPool
	{
		/// <summary>
		/// Forwards the task unless a stop is incomplete; otherwise discards it.
		/// </summary>
		public void Put(CancellableTask task)
			{
				lock (access)
					{
						if (stopping.Value is { IsCompleted: false }) return;

						stopping.Value = null;
						source.Put(task);
					}
			}

		/// <summary>
		/// Returns the current incomplete stop or starts and stores a new one.
		/// </summary>
		public Task Stop()
			{
				lock (access)
					{
						if (stopping.Value is { IsCompleted: false } current)
							return current;

						var stopped = source.Stop();
						stopping.Value = stopped;
						return stopped;
					}
			}
	}