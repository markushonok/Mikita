using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class ErrorMatchTaskTest
	{
		[Fact]
		public static async Task OperationFaultIsReportedThroughErrorHook()
			{
				var failure = new InvalidOperationException("lost reply");
				var source = MatchTask.From
					(
						() => Task.FromException<Match<ErrorHook>>
							(
								failure
							)
					);
				var decorated = source.WithExceptionMatch;
				var hook = new ErrorHook();

				await decorated.Match(hook);

				var errorAction = hook.ErrorAction.ShouldNotBeNull();
				var thrown = Should.Throw<InvalidOperationException>
					(
						errorAction
					);
				thrown.ShouldBeSameAs(failure);
			}

		[Fact]
		public static Task DispatchFaultPropagatesUnchanged()
			{
				var failure = new InvalidOperationException("bad hook");
				var source = MatchTask.From
					(
						(Match<ErrorHook>) (_ => throw failure)
					);
				var decorated = source.WithExceptionMatch;
				var hook = new ErrorHook();

				Should
					.Throw<InvalidOperationException>(() => decorated.Match(hook))
					.ShouldBeSameAs(failure);
				hook.ErrorAction.ShouldBeNull();
				return Task.CompletedTask;
			}

		[Fact]
		public static async Task SuccessfulOutcomeIsDispatchedUnchanged()
			{
				var source = MatchTask.From<ErrorHook>
					(
						static hook => hook.Complete()
					);
				var decorated = source.WithExceptionMatch;
				var hook = new ErrorHook();

				await decorated.Match(hook);

				hook.IsCompleted.ShouldBeTrue();
				hook.ErrorAction.ShouldBeNull();
			}

		private sealed class ErrorHook: IErrorHook
			{
				public bool IsCompleted { get; private set; }

				public Action? ErrorAction { get; private set; }

				public void Complete()
					=> IsCompleted = true;

				public void Error(string message)
					=> throw new InvalidOperationException(message);

				public void Error(Action @throw)
					=> ErrorAction = @throw;
			}
	}