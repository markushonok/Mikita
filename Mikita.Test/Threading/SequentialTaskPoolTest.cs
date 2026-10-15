using Mikita.Logging;
using Mikita.Threading.Pools;
using System.Collections.Concurrent;

namespace Mikita.Test.Threading;

public static class SequentialTaskPoolTest
	{
		[Fact]
		public static async Task TasksExecuteInSubmissionOrderWithoutOverlap()
			{
				var pool = PoolWith([]);
				var release = Gate();
				var finished = Gate();
				var calls = new ConcurrentQueue<int>();
				pool.Put
					(
						async cancel =>
							{
								calls.Enqueue(1);
								await release.Task;
								calls.Enqueue(2);
							}
					);
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(3);
								return Task.CompletedTask;
							}
					);
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(4);
								finished.SetResult();
								return Task.CompletedTask;
							}
					);

				calls.ShouldBe([1]);
				release.SetResult();
				await finished.Task.WaitAsync(TimeSpan.FromSeconds(5));
				await pool.Stop();

				calls.ShouldBe([1, 2, 3, 4]);
			}

		[Fact]
		public static async Task StopCancelsRunningWork()
			{
				var messages = new List<string>();
				var pool = PoolWith(messages);
				var started = Gate();
				pool.Put
					(
						async cancel =>
							{
								started.SetResult();
								await Task.Delay(Timeout.InfiniteTimeSpan, cancel);
							}
					);
				await started.Task.WaitAsync(TimeSpan.FromSeconds(5));

				await pool.Stop().WaitAsync(TimeSpan.FromSeconds(5));

				messages.ShouldBeEmpty();
			}

		[Fact]
		public static async Task StopWaitsForRunningWorkAndDiscardsWaitingWork()
			{
				var pool = PoolWith([]);
				var cancelled = Gate();
				var release = Gate();
				var calls = new ConcurrentQueue<int>();
				pool.Put
					(
						async cancel =>
							{
								using var registration = cancel.Register
									(
										() => cancelled.SetResult()
									);
								await release.Task;
							}
					);
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(1);
								return Task.CompletedTask;
							}
					);

				var stop = pool.Stop();
				await cancelled.Task.WaitAsync(TimeSpan.FromSeconds(5));
				stop.IsCompleted.ShouldBeFalse();
				release.SetResult();
				await stop.WaitAsync(TimeSpan.FromSeconds(5));

				calls.ShouldBeEmpty();
			}

		[Fact]
		public static async Task FailureDoesNotBlockTheNextTask()
			{
				var messages = new List<string>();
				var pool = PoolWith(messages);
				var fail = Gate();
				var finished = Gate();
				pool.Put
					(
						async cancel =>
							{
								await fail.Task;
								throw new InvalidOperationException("Expected failure");
							}
					);
				pool.Put
					(
						cancel =>
							{
								finished.SetResult();
								return Task.CompletedTask;
							}
					);

				fail.SetResult();
				await finished.Task.WaitAsync(TimeSpan.FromSeconds(5));
				await pool.Stop();

				messages.Count.ShouldBe(1);
				messages[0].ShouldContain("Expected failure");
			}

		[Fact]
		public static async Task AcceptsANewGroupAfterStop()
			{
				var pool = PoolWith([]);
				var calls = new ConcurrentQueue<int>();
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(1);
								return Task.CompletedTask;
							}
					);
				await pool.Stop();
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(2);
								return Task.CompletedTask;
							}
					);
				await pool.Stop();

				calls.ShouldBe([1, 2]);
			}

		[Fact]
		public static async Task RunningTaskCanQueueMoreWork()
			{
				var pool = PoolWith([]);
				var finished = Gate();
				var calls = new ConcurrentQueue<int>();
				pool.Put
					(
						cancel =>
							{
								calls.Enqueue(1);
								pool.Put
									(
										nextCancel =>
											{
												calls.Enqueue(3);
												finished.SetResult();
												return Task.CompletedTask;
											}
									);
								calls.Enqueue(2);
								return Task.CompletedTask;
							}
					);

				await finished.Task.WaitAsync(TimeSpan.FromSeconds(5));
				await pool.Stop();

				calls.ShouldBe([1, 2, 3]);
			}

		private static ITaskPool PoolWith(ICollection<string> messages)
			=> TaskPool.NewWith(new RecordingLog(messages))
				.WithSequentialExecution.WithSerialAccess;

		private static TaskCompletionSource Gate()
			=> new
				(
					TaskCreationOptions.RunContinuationsAsynchronously
				);

		private sealed class RecordingLog(ICollection<string> messages): ILog
			{
				public void Write(string message)
					=> messages.Add(message);
			}
	}