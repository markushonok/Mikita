using Mikita.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mikita.Routines;
using Mikita.Structs.Referring;
using System.Linq;

namespace Mikita.Threading.Pools;

public sealed class TaskPool
	(
		ICollection<Task> tasks,
		IRef<CancellationTokenSource?> cancelSource,
		ILog log
	)
	: ITaskPool
	{
		public void Put(CancellableTask task)
			{
				if (cancelSource.Value is null)
					{
						cancelSource.SetTo(new CancellationTokenSource());
					}
				else if (cancelSource.Value.IsCancellationRequested) return;

				var taskSource = new TaskCompletionSource();
				tasks.Add(taskSource.Task);
				RunAndCleanup(task, taskSource).Forget();
			}

		private async Task RunAndCleanup
			(
				CancellableTask task,
				TaskCompletionSource taskSource
			)
			{
				try
					{
						await task(cancelSource.Value!.Token);
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
						tasks.Remove(taskSource.Task);
						taskSource.SetResult();
					}
			}

		public async Task Stop()
			{
				if (cancelSource.Value is null) return;
				await cancelSource.Value.CancelAsync();
				await Task.WhenAll(tasks.ToArray());
				cancelSource.Value.Dispose();
				cancelSource.SetTo(null);
			}
	}