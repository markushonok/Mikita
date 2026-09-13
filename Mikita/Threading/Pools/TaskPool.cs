using Mikita.Logging;
using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

/// <summary>
/// Runs one reusable group of cancellable tasks.
/// </summary>
/// <remarks>
/// Public <see cref="Put"/> and <see cref="Stop"/> calls must not overlap.
/// Internal task completions may run concurrently: their task-list mutations
/// are synchronized. Task cancellation is ignored; other task failures are
/// logged. Use <see cref="SerialAccessTaskPool"/> when callers require
/// concurrent access.
/// </remarks>
/// <param name="tasks">
/// Tasks whose execution has not finished bookkeeping.
/// </param>
/// <param name="cancelSource">Cancellation source of the current group.</param>
/// <param name="log">Receives unhandled task failures.</param>
/// <param name="tasksAccess">
/// Serializes internal access to <paramref name="tasks"/>.
/// </param>
public sealed class TaskPool
	(
		ICollection<Task> tasks,
		IRef<CancellationTokenSource?> cancelSource,
		ILog log,
		Lock tasksAccess
	)
	: ITaskPool
	{
		/// <summary>
		/// Starts a task with the current group's token. Rejects it if that token
		/// is already being cancelled.
		/// </summary>
		public void Put(CancellableTask task)
			{
				if (cancelSource.Value is null)
					{
						cancelSource.SetTo(new CancellationTokenSource());
					}
				else if (cancelSource.Value.IsCancellationRequested) return;

				var cancel = cancelSource.Value!.Token;
				var completion = new TaskCompletionSource
					(
						TaskCreationOptions.RunContinuationsAsynchronously
					);

				lock (tasksAccess) tasks.Add(completion.Task);
				Run(task, cancel, completion).Forget();
			}

		private async Task Run
			(
				CancellableTask task,
				CancellationToken cancel,
				TaskCompletionSource completion
			)
			{
				try
					{
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
						lock (tasksAccess) tasks.Remove(completion.Task);
						completion.SetResult();
					}
			}

		/// <summary>
		/// Cancels and waits for the current snapshot, resets the group, then
		/// propagates the first cancellation-callback or waiting failure.
		/// </summary>
		public async Task Stop()
			{
				var source = cancelSource.Value;
				if (source is null) return;

				Task[] pending;
				lock (tasksAccess) pending = tasks.ToArray();

				var cancelException = await source.CancelAsync().WaitException;
				var taskException = await Task.WhenAll(pending).WaitException;

				source.Dispose();
				cancelSource.SetTo(null);

				var exception = cancelException ?? taskException;
				if (exception is object)
					ExceptionDispatchInfo.Capture(exception).Throw();
			}
	}