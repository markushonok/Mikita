using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Caching;

public sealed class AsyncRetryingValid<T>
	(
		IAsyncValid<T> source,
		Func<int> retries,
		TimeSpan interval
	)
	: IAsyncValid<T>
	{

		public async Task<T> Value
			(
				CancellationToken cancel = default
			)
			{
				var remaining = retries();

				while (true)
					{
						try
							{
								return await source.Value(cancel);
							}
						catch (OperationCanceledException)
							{
								throw;
							}
						catch when (remaining > 0)
							{
								source.Invalidate();
								remaining--;

								if (interval > TimeSpan.Zero)
									await Task.Delay(interval, cancel);
							}
					}
			}

		public void Invalidate()
			=> source.Invalidate();
	}