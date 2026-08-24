using Mikita.Structs.Maps;
using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class SafeTaskMatchingTest
	{
		[Fact]
		public static async Task FailureUsesMappedMessage()
			{
				var failure = new ExpectedException("Operation failed.");
				var origin = MatchTask.From
					(
						Task.FromException<Action<IFailureHook>>(failure)
					);
				var operation = origin.With
					(
						Map.Of<Exception, string>
							(
								exception => exception.Message
							)
					);
				var outcomes = new Outcomes();

				await operation.Match(outcomes);

				outcomes.Failure.ShouldBe(failure.Message);
			}

		[Fact]
		public static async Task FailureUsesFixedMessage()
			{
				var origin = MatchTask.From
					(
						Task.FromException<Action<IFailureHook>>
							(
								new ExpectedException("Operation failed.")
							)
					);
				var operation = origin.With("Unavailable.");
				var outcomes = new Outcomes();

				await operation.Match(outcomes);

				outcomes.Failure.ShouldBe("Unavailable.");
			}

		[Fact]
		public static async Task CancellationPropagates()
			{
				var cancellation = new CancellationToken(canceled: true);
				var origin = MatchTask.From
					(
						Task.FromCanceled<Action<IFailureHook>>(cancellation)
					);
				var operation = origin.With("Unavailable.");
				var outcomes = new Outcomes();

				await Should.ThrowAsync<OperationCanceledException>
					(
						() => operation.Match(outcomes)
					);

				outcomes.Failure.ShouldBeNull();
			}

		[Fact]
		public static async Task OutcomeFailurePropagates()
			{
				var failure = new ExpectedException("Outcome failed.");
				var origin = MatchTask.From<IFailureHook>
					(
						_ => throw failure
					);
				var operation = origin.With("Unavailable.");

				var observed = await Should.ThrowAsync<ExpectedException>
					(
						() => operation.Match(new Outcomes())
					);

				observed.ShouldBeSameAs(failure);
			}

		private sealed class Outcomes: IFailureHook
			{
				public string? Failure { get; private set; }

				public void FailWith(string message)
					=> Failure = message;
			}

		private sealed class ExpectedException(string message)
			: Exception(message);
	}