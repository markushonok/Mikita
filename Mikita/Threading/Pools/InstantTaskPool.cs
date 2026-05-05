using Mikita.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mikita.Routines;
using System.Linq;

namespace Mikita.Threading.Pools;

public sealed class InstantTaskPool
	(
		ICollection<Task> tasks,
		CancellationTokenSource cancelSource,
		ILog log
	)
	: ITaskPool
	{
		public void Put
			(
				CancellableTask task
			)
			{
				if (cancelSource.IsCancellationRequested) return;
				tasks.Add(RemoveAfter(Handled(task)));
			}

		private async Task Handled
			(
				CancellableTask task
			)
			{
				try
					{
						await task(cancelSource.Token);
					}
				catch (OperationCanceledException)
					{
					}
				catch (Exception exception)
					{
						log.Write(exception.ToString());
					}
			}

		private Task RemoveAfter
			(
				Task task
			)
			=> task.ContinueWith
				(
					tasks.Remove,
					TaskContinuationOptions.ExecuteSynchronously
				);

		public async Task Stop()
			{
				await cancelSource.CancelAsync();
				await Task.WhenAll(tasks.ToArray());
			}
	}