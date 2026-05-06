using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading;

public static class TaskTimeout
	{
		public static async Task<T> WithTimeout<T>
			(
				this Task<T> task,
				TimeSpan timeout,
				CancellationToken cancel = default
			)
			{
				using var timeoutCancelSource = CancellationTokenSource
					.CreateLinkedTokenSource(cancel);

				var timeoutTask = Task.Delay
					(
						timeout,
						timeoutCancelSource.Token
					);

				var completedTask = await Task.WhenAny(task, timeoutTask);

				if (completedTask == timeoutTask)
					{
						cancel.ThrowIfCancellationRequested();
						throw TimeoutException(timeout);
					}

				await timeoutCancelSource.CancelAsync();
				return await task;
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