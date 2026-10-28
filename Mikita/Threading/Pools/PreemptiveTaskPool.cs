using Mikita.Logging;
using Mikita.Structs.Referring;
using Mikita.Routines;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Runs at most one task at a time and keeps only the latest requested task.
/// A request cancels the running task, waits for it to finish, and only then
/// starts itself, so effects of an earlier request never follow a later one.
/// </summary>
/// <remarks>
/// Cancellation is cooperative: a task that ignores its token delays the new
/// request until it finishes. The contract does not guarantee concurrent API
/// access; compose with <see cref="SerialAccessTaskPool"/> when callers may
/// overlap.
/// </remarks>
public sealed partial class PreemptiveTaskPool
	{
		/// <summary>
		/// Creates a pool that cancels and awaits the running task before
		/// starting the latest request, without concurrent public access.
		/// </summary>
		public static ITaskPool NewWith
			(
				ILog log
			)
			=> new PreemptiveTaskPool
				(
					tail: Ref<Task>.Null,
					source: Ref<CancellationTokenSource>.Null,
					log,
					new Lock()
				);
	}

public sealed partial class PreemptiveTaskPool
	(
		IRef<Task?> tail,
		IRef<CancellationTokenSource?> source,
		ILog log,
		Lock access
	)
	: ITaskPool
	{
		/// <summary>
		/// Cancels the running task and starts this one after it finishes.
		/// </summary>
		public void Put(CancellableTask task)
			{
				Task? previous;
				CancellationTokenSource? previousSource;
				TaskCompletionSource completion;
				CancellationTokenSource current;

				lock (access)
					{
						previous = tail.Value;
						previousSource = source.Value;
						completion = new TaskCompletionSource
							(
								TaskCreationOptions
									.RunContinuationsAsynchronously
							);
						current = new CancellationTokenSource();
						tail.SetTo(completion.Task);
						source.SetTo(current);
					}

				RequestCancel(previousSource);

				Run
					(
						previous,
						completion,
						task,
						current
					)
					.Forget();
			}

		/// <summary>
		/// Cancels and awaits the running task, then publishes a cancellation or
		/// task failure.
		/// </summary>
		public async Task Stop()
			{
				CancellationTokenSource? current;
				Task? running;

				lock (access)
					{
						current = source.Value;
						running = tail.Value;
					}

				var cancellation = await Cancel(current);

				var failure = running is object
					? await running.WaitException
					: null;

				lock (access)
					{
						if (ReferenceEquals(tail.Value, running))
							{
								tail.SetTo(null);
								source.SetTo(null);
							}
					}

				var exception = cancellation ?? failure;
				if (exception is object)
					ExceptionDispatchInfo.Capture(exception).Throw();
			}

		private static void RequestCancel
			(
				CancellationTokenSource? current
			)
			{
				if (current is not null)
					Cancel(current).Forget();
			}

		private static async Task<Exception?> Cancel
			(
				CancellationTokenSource? current
			)
			{
				if (current is null) return null;

				try
					{
						return await current
							.CancelAsync()
							.WaitException;
					}
				catch (ObjectDisposedException)
					{
						return null;
					}
			}

		private static async Task IgnoreCancellation(Task task)
			{
				try {await task;}
				catch (OperationCanceledException) {}
			}

		private async Task Run
			(
				Task? previous,
				TaskCompletionSource completion,
				CancellableTask task,
				CancellationTokenSource current
			)
			{
				try
					{
						if (previous is object)
							await IgnoreCancellation(previous);

						lock (access)
							{
								if (!ReferenceEquals(tail.Value, completion.Task))
									return;
							}

						await task(current.Token);
					}
				catch (OperationCanceledException)
					{
					}
				catch (Exception exception)
					{
						log.Write(exception.ToString());
					}
				finally
					{
						completion.SetResult();
					}
			}
	}