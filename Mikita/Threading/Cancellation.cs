using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading;

public static class Cancellation
	{
		public static ConfiguredCancelableAsyncEnumerable<T> With<T>
			(
				this IAsyncEnumerable<T> source,
				CancellationToken cancellation
			)
			=> source.WithCancellation(cancellation);
	}