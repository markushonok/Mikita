using Mikita.Routines;
using Mikita.Structs.Referring;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Forwards to another <see cref="ITaskPool"/> until it is stopped,
/// after which it rejects further work.
/// </summary>
/// <remarks>
/// The contract does not guarantee concurrent API access. Compose with
/// <see cref="SerialAccessTaskPool"/> when callers may overlap.
/// </remarks>
/// <param name="source">Pool receiving forwarded calls.</param>
/// <param name="released">Whether the pool has been stopped.</param>
public sealed class SingleUseTaskPool
	(
		ITaskPool source,
		IRef<bool> released
	)
	: ITaskPool
	{
		/// <summary>
		/// Forwards the task unless the pool has been stopped.
		/// </summary>
		public void Put(CancellableTask task)
			{
				if (released.Value) return;
				source.Put(task);
			}

		/// <summary>
		/// Stops the source and rejects further work.
		/// </summary>
		public Task Stop()
			{
				released.SetTo(true);
				return source.Stop();
			}
	}