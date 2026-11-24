using Mikita.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Requests;

public static class DeadlineMatchTask
	{
		public static IMatchTask<T> From<T>
			(
				Func<CancellationToken, IMatchTask<T>> source,
				TimeSpan duration,
				Action<T, TimeSpan> timeout,
				CancellationToken cancel = default
			)
			{
				cancel.ThrowIfRequested();

				var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancel);
				deadline.CancelAfter(duration);

				try
					{
						var origin = source(deadline.Token);
						return new DeadlineMatchTask<T>
							(
								origin,
								Await(origin, deadline, duration, cancel),
								duration,
								timeout
							);
					}
				catch
					{
						deadline.Dispose();
						throw;
					}
			}

		private static async Task Await<T>
			(
				IMatchTask<T> source,
				CancellationTokenSource deadline,
				TimeSpan duration,
				CancellationToken cancel
			)
			{
				using (deadline)
					{
						try
							{
								await source;
							}
						catch (OperationCanceledException)
							when (!cancel.IsCancellationRequested)
							{
								throw DeadlineTask.TimeoutException(duration);
							}
					}
			}
	}

public sealed class DeadlineMatchTask<T>
	(
		IMatchTask<T> source,
		Task completion,
		TimeSpan duration,
		Action<T, TimeSpan> timeout
	)
	: IMatchTask<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> completion.GetAwaiter();

		public async Task Match(T outcomes)
			{
				try
					{
						await completion;
					}
				catch (TimeoutException)
					{
						timeout(outcomes, duration);
						return;
					}

				await source.Match(outcomes);
			}
	}