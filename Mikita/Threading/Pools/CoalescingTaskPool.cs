using Mikita.Logging;
using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Runs at most one task at a time and keeps only the latest requested task.
/// A request that arrives while another task runs or waits supersedes the
/// waiting request, so the most recent request runs next.
/// </summary>
/// <remarks>
/// The contract does not guarantee concurrent API access. Compose with
/// <see cref="SerialAccessTaskPool"/> when callers may overlap.
/// </remarks>
/// <param name="tail">Completion of the latest request.</param>
/// <param name="cancel">Cancellation source of the current group.</param>
/// <param name="log">Receives unhandled task failures.</param>
/// <param name="access">
/// Serializes internal access to <paramref name="tail"/> and
/// <paramref name="cancel"/>, including asynchronous continuations.
/// </param>
public sealed partial class CoalescingTaskPool
	{
		/// <summary>
		/// Creates a pool that runs one task at a time and keeps only the
		/// latest request, without concurrent public access.
		/// </summary>
		public static ITaskPool NewWith
			(
				ILog log
			)
			=> new CoalescingTaskPool
				(
					tail: Ref<Task>.Null,
					cancel: Ref<CancellationTokenSource>.Null,
					log,
					access: new Lock()
				);
	}

public sealed partial class CoalescingTaskPool
	(
		IRef<Task?> tail,
		IRef<CancellationTokenSource?> cancel,
		ILog log,
		Lock access
	)
	: ITaskPool
	{
		/// <summary>
		/// Starts a task, or replaces the waiting one when a task is already
		/// running.
		/// </summary>
		public void Put(CancellableTask task)
			{
				Task? previous;
				TaskCompletionSource completion;
				CancellationToken token;

				lock (access)
					{
						previous = tail.Value;
						completion = new TaskCompletionSource
							(
								TaskCreationOptions
									.RunContinuationsAsynchronously
							);

						tail.SetTo(completion.Task);
						token = CancelSource.Token;
					}

				Run(previous, completion, task, token).Forget();
			}

		/// <summary>
		/// Cancels and waits for the running task, discards the waiting one,
		/// then publishes a cancellation or task failure.
		/// </summary>
		public async Task Stop()
			{
				CancellationTokenSource? source;
				Task? current;

				lock (access)
					{
						source = cancel.Value;
						current = tail.Value;

						tail.SetTo(null);
						cancel.SetTo(null);
					}

				if (source is null) return;

				var cancelException = await source.CancelAsync().WaitException;

				var taskException = current is object
					? await current.WaitException
					: null;

				source.Dispose();

				var exception = cancelException ?? taskException;
				if (exception is object)
					ExceptionDispatchInfo.Capture(exception).Throw();
			}

		private CancellationTokenSource CancelSource
			{
				get
					{
						var source = cancel.Value;

						if (source is null || source.IsCancellationRequested)
							{
								source = new CancellationTokenSource();
								cancel.SetTo(source);
							}

						return source;
					}
			}

		private async Task Run
			(
				Task? previous,
				TaskCompletionSource completion,
				CancellableTask task,
				CancellationToken cancel
			)
			{
				try
					{
						if (previous is object)
							{
								try
									{
										await previous;
									}
								catch (OperationCanceledException) {}
							}

						lock (access)
							{
								if (!ReferenceEquals(tail.Value, completion.Task))
									return;
							}

						await task(cancel);
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