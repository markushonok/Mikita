using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class TaskThrowingTest
	{
		[Fact]
		public static async Task SynchronousThrowBecomesFailure()
			{
				var failure = new ExpectedException();
				var task = failure.DoWithThrowAsTask
					(_ => throw failure);

				var observed = await Should.ThrowAsync<ExpectedException>
					(() => task);

				observed.ShouldBeSameAs(failure);
			}

		[Fact]
		public static void SynchronousCancellationIsFailure()
			{
				var cancellation = new OperationCanceledException();
				var task = cancellation.DoWithThrowAsTask
					(_ => throw cancellation);

				task.IsFaulted.ShouldBeTrue();
			}

		[Fact]
		public static void PreservesReturnedTask()
			{
				var expected = Task.FromResult(1);
				var actual = expected.DoWithThrowAsTask(task => task);

				actual.ShouldBeSameAs(expected);
			}

		private sealed class ExpectedException: Exception;
	}