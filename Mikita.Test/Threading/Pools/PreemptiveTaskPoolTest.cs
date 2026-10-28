using Mikita.Logging;
using Mikita.Structs.Referring;
using Mikita.Threading.Pools;

namespace Mikita.Test.Threading.Pools;

public static class PreemptiveTaskPoolTest
	{
		[Fact]
		public static async Task LatestRequestStartsAfterTheCancelledOne()
			{
				var events = new List<string>();
				var gate = NewCompletion;
				var pool = PreemptiveTaskPool.NewWith(new Log());

				async Task First(CancellationToken cancel)
					{
						try
							{
								await gate.Task.WaitAsync(cancel);
								events.Add("first finished");
							}
						catch (OperationCanceledException)
							{
								events.Add("first cancelled");
								throw;
							}
					}

				pool.Put(First);
				pool.Put(cancel =>
					{
						events.Add("second");
						return Task.CompletedTask;
					});

				await pool.Stop();

				events.ShouldBe(["first cancelled", "second"]);
			}

		[Fact]
		public static async Task OnlyTheLatestWaitingRequestRuns()
			{
				var events = new List<string>();
				var gate = NewCompletion;
				var pool = PreemptiveTaskPool.NewWith(new Log());

				pool.Put(cancel => gate.Task);
				pool.Put(cancel =>
					{
						events.Add("second");
						return Task.CompletedTask;
					});
				pool.Put(cancel =>
					{
						events.Add("third");
						return Task.CompletedTask;
					});

				gate.SetResult();
				await pool.Stop();

				events.ShouldBe(["third"]);
			}

		[Fact]
		public static async Task StopCancelsAndWaitsForTheRunningTask()
			{
				var isCancelled = Ref.To(false);
				var pool = PreemptiveTaskPool.NewWith(new Log());

				async Task Waiting(CancellationToken cancel)
					{
						try
							{
								await Task.Delay(Timeout.Infinite, cancel);
							}
						catch (OperationCanceledException)
							{
								isCancelled.SetTo(true);
								throw;
							}
					}

				pool.Put(Waiting);
				await pool.Stop();

				isCancelled.Value.ShouldBeTrue();
			}

		[Fact]
		public static async Task FailureIsLoggedAndDoesNotStopThePool()
			{
				var log = new Log();
				var pool = PreemptiveTaskPool.NewWith(log);

				pool.Put(_ => throw new InvalidOperationException("failed"));
				pool.Put(_ => Task.CompletedTask);
				await pool.Stop();

				log.Messages.Single().ShouldContain("failed");
			}

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);

		private sealed class Log: ILog
			{
				public IReadOnlyList<string> Messages => messages;

				public void Write(string message) => messages.Add(message);

				private readonly List<string> messages = [];
			}
	}