using Mikita.Routines;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Owns cancellable tasks as a reusable stopping group.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="Put"/> starts work in the current group. <see cref="Stop"/>
/// cancels and waits for that group; implementations may accept a new group
/// after the returned task completes.
/// </para>
/// <para>
/// The contract does not guarantee concurrent API access. Implementations must
/// make their own asynchronous bookkeeping safe. Task code that may call this
/// API concurrently still requires a thread-safe implementation such as
/// <see cref="SerialAccessTaskPool"/>.
/// </para>
/// </remarks>
public interface ITaskPool
	{
		/// <summary>
		/// Starts <paramref name="task"/> in the current group and returns before
		/// it completes. A pool may reject work while its group is stopping.
		/// </summary>
		void Put(CancellableTask task);

		/// <summary>
		/// Requests cancellation of the current group and completes after its
		/// accepted tasks stop. Cancellation of managed work is a successful stop.
		/// </summary>
		Task Stop();
	}