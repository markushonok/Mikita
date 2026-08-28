using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class TaskMatchingTest
	{
		[Fact]
		public static async Task WaitsForOperation()
			{
				var outcomes = new Outcomes();
				var outcome = PendingOutcome();
				var operation = MatchTask.From(outcome.Task);
				var match = operation.Match(outcomes);

				match.IsCompleted.ShouldBeFalse();
				outcome.SetResult(x => x.Complete());
				await match;

				outcomes.Completions.ShouldBe(1);
			}

		[Fact]
		public static async Task SupportsRepetition()
			{
				var first = new Outcomes();
				var second = new Outcomes();
				var operation = MatchTask.From<IOutcomes>
					(
						x => x.Complete()
					);

				await operation.Match(first);
				await operation.Match(second);

				first.Completions.ShouldBe(1);
				second.Completions.ShouldBe(1);
			}

		[Fact]
		public static async Task SupportsConcurrency()
			{
				var first = new Outcomes();
				var second = new Outcomes();
				var outcome = PendingOutcome();
				var operation = MatchTask.From(outcome.Task);
				var matches = Task.WhenAll
					(
						operation.Match(first),
						operation.Match(second)
					);

				outcome.SetResult(x => x.Complete());
				await matches;

				first.Completions.ShouldBe(1);
				second.Completions.ShouldBe(1);
			}

		[Fact]
		public static async Task PropagatesFailureWithoutDispatch()
			{
				var failure = new ExpectedException();
				var outcome = Task.FromException<Match<IOutcomes>>(failure);
				var operation = MatchTask.From(outcome);
				var outcomes = new Outcomes();

				var observed = await Should.ThrowAsync<ExpectedException>
					(
						() => operation.Match(outcomes)
					);

				observed.ShouldBeSameAs(failure);
				outcomes.Completions.ShouldBe(0);
			}

		[Fact]
		public static async Task PropagatesCancellationWithoutDispatch()
			{
				var cancellation = new CancellationToken(canceled: true);
				var outcome = Task.FromCanceled<Match<IOutcomes>>(cancellation);
				var operation = MatchTask.From(outcome);
				var outcomes = new Outcomes();

				await Should.ThrowAsync<OperationCanceledException>
					(
						() => operation.Match(outcomes)
					);

				outcomes.Completions.ShouldBe(0);
			}

		[Fact]
		public static async Task OutcomeFailureIsObservedOnlyByMatching()
			{
				var failure = new ExpectedException();
				var operation = MatchTask.From<IOutcomes>
					(
						_ => throw failure
					);

				var observed = await Should.ThrowAsync<ExpectedException>
					(
						() => operation.Match(new Outcomes())
					);

				observed.ShouldBeSameAs(failure);
				await operation;
			}

		private static TaskCompletionSource<Match<IOutcomes>> PendingOutcome()
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);

		private interface IOutcomes
			{
				void Complete();
			}

		private sealed class Outcomes: IOutcomes
			{
				public int Completions { get; private set; }

				public void Complete()
					=> Completions++;
			}

		private sealed class ExpectedException: Exception;
	}