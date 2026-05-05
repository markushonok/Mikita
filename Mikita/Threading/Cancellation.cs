using Mikita.Routines;
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
				CancellationToken cancel
			)
			=> source.WithCancellation(cancel);

		extension(CancellationToken cancel)
			{
				public void ThrowIfRequested()
					=> cancel.ThrowIfCancellationRequested();

				public CancellableTask With
					(
						CancellableTask task
					)
					=> async sourceCancel =>
						{
							using var combined = CancellationTokenSource
								.CreateLinkedTokenSource(sourceCancel, cancel);

							await task(combined.Token);
						};
			}

	}