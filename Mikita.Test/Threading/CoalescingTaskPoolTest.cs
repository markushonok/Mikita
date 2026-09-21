using Mikita.Logging;
using Mikita.Threading.Pools;

namespace Mikita.Test.Threading;

public static class CoalescingTaskPoolTest
	{
		[Fact]
		public static async Task RunsOneAtATime()
			{
				var pool = CoalescingTaskPool.NewWith(Log.Debug);
				var release = Completion();
				var started = Completion();
				var second = Completion();

				pool.Put(async cancel =>
					{
						started.TrySetResult();
						await release.Task;
					});
				await started.Task;

				pool.Put(cancel =>
					{
						second.TrySetResult();
						return Task.CompletedTask;
					});

				await Task.Delay(50);

				second.Task.IsCompleted.ShouldBeFalse();

				release.SetResult();
				await second.Task;
				await pool.Stop();
			}

		[Fact]
		public static async Task SupersedesWaitingRequest()
			{
				var pool = CoalescingTaskPool.NewWith(Log.Debug);
				var release = Completion();
				var started = Completion();
				var superseded = Completion();
				var latest = Completion();

				pool.Put(async cancel =>
					{
						started.TrySetResult();
						await release.Task;
					});
				await started.Task;

				pool.Put(cancel =>
					{
						superseded.TrySetResult();
						return Task.CompletedTask;
					});
				pool.Put(cancel =>
					{
						latest.TrySetResult();
						return Task.CompletedTask;
					});

				release.SetResult();
				await latest.Task;
				await pool.Stop();

				superseded.Task.IsCompleted.ShouldBeFalse();
			}

		[Fact]
		public static async Task StopWaitsForRunning()
			{
				var pool = CoalescingTaskPool.NewWith(Log.Debug);
				var release = Completion();
				var started = Completion();

				pool.Put(async cancel =>
					{
						started.TrySetResult();
						await release.Task;
					});
				await started.Task;

				var stopped = pool.Stop();
				await Task.Delay(50);

				stopped.IsCompleted.ShouldBeFalse();

				release.SetResult();
				await stopped;
			}

		[Fact]
		public static async Task StopDiscardsWaiting()
			{
				var pool = CoalescingTaskPool.NewWith(Log.Debug);
				var release = Completion();
				var started = Completion();
				var waiting = Completion();

				pool.Put(async cancel =>
					{
						started.TrySetResult();
						await release.Task;
					});
				await started.Task;

				pool.Put(cancel =>
					{
						waiting.TrySetResult();
						return Task.CompletedTask;
					});

				var stopped = pool.Stop();
				release.SetResult();
				await stopped;

				waiting.Task.IsCompleted.ShouldBeFalse();
			}

		[Fact]
		public static async Task StopWaitsAndPublishesCancellationFailure()
			{
				var pool = CoalescingTaskPool.NewWith(Log.Debug);
				var release = Completion();
				var started = Completion();
				var finished = Completion();

				pool.Put(async cancel =>
					{
						using var registration = cancel.Register
							(
								() => throw new InvalidOperationException
									("Callback failed.")
							);

						started.TrySetResult();
						await release.Task;
						finished.TrySetResult();
					});
				await started.Task;

				var stopped = pool.Stop();
				await Task.Delay(50);

				stopped.IsCompleted.ShouldBeFalse();

				release.SetResult();
				var failure = await Record.ExceptionAsync(() => stopped);

				failure.ShouldNotBeNull();
				finished.Task.IsCompleted.ShouldBeTrue();
			}

		private static TaskCompletionSource Completion()
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);
	}
