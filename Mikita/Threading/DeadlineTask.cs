using System;
using System.Threading;
using System.Threading.Tasks;
using Mikita.Routines;

namespace Mikita.Threading;

public static class DeadlineTask
	{
		public static async Task<T> From<T>
			(
				CancellableTask<T> task,
				TimeSpan duration,
				CancellationToken cancel = default
			)
			{
				using var deadline = CancellationTokenSource
					.CreateLinkedTokenSource(cancel);

				deadline.CancelAfter(duration);

				try
					{
						return await task(deadline.Token);
					}
				catch (OperationCanceledException) when (!cancel.IsCancellationRequested)
					{
						throw TimeoutException(duration);
					}
			}

		public static Exception TimeoutException
			(
				TimeSpan timeout
			)
			=> new TimeoutException
				(
					$"The operation has exceeded the timeout of {timeout}."
				);
	}
