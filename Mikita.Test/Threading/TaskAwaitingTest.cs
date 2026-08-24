using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class TaskAwaitingTest
	{
		[Fact]
		public static async Task WaitsForOperation()
			{
				var outcome = PendingOutcome();
				var operation = MatchTask.From(outcome.Task);

				operation.GetAwaiter().IsCompleted.ShouldBeFalse();
				outcome.SetResult(_ => {});
				await operation;
			}

		[Fact]
		public static async Task DoesNotMatch()
			{
				var matches = 0;
				var operation = MatchTask.From<object>(_ => matches++);
				await operation;
				matches.ShouldBe(0);
			}

		[Fact]
		public static async Task PropagatesFailure()
			{
				var failure = new ExpectedException();
				var outcome = Task.FromException<Action<object>>(failure);
				var operation = MatchTask.From(outcome);

				var observed = await Should.ThrowAsync<ExpectedException>
					(
						async () => await operation
					);

				observed.ShouldBeSameAs(failure);
			}

		[Fact]
		public static async Task PropagatesCancellation()
			{
				var cancellation = new CancellationToken(canceled: true);
				var outcome = Task.FromCanceled<Action<object>>(cancellation);
				var operation = MatchTask.From(outcome);

				await Should.ThrowAsync<OperationCanceledException>
					(
						async () => await operation
					);
			}

		private static TaskCompletionSource<Action<object>> PendingOutcome()
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);

		private sealed class ExpectedException: Exception;
	}
