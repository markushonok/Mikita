using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Represents an asynchronous operation whose semantic outcome is a reusable
/// <see cref="Match{T}"/>.
/// </summary>
/// <remarks>
/// Awaiting the operation only waits for its completion. <see cref="Match"/>
/// waits for that same operation and then invokes its match. Calling
/// <see cref="Match"/> must neither start nor repeat the underlying operation.
/// Operation faults and cancellation are observed by both forms of waiting;
/// exceptions raised while dispatching an outcome are observed only through
/// <see cref="Match"/>.
/// </remarks>
public interface IMatchTask<in T>: ITask
	{
		new TaskAwaiter GetAwaiter();

		/// <summary>
		/// Waits for the operation and invokes its saved match.
		/// </summary>
		/// <remarks>
		/// The same outcome may be matched more than once. Concurrent calls may
		/// dispatch it concurrently to different outcome hooks.
		/// </remarks>
		Task Match(T outcomes);
	}