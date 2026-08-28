using Mikita.Threading;

namespace Mikita.Test.Threading;

public static class TaskOperationTest
	{
		[Fact]
		public static async Task RunsOnce()
			{
				var executions = 0;

				var release = new TaskCompletionSource
					(
						TaskCreationOptions.RunContinuationsAsynchronously
					);

				async Task<Match<object>> Outcome()
					{
						executions++;
						await release.Task;
						return _ => {};
					}

				var operation = MatchTask.From(Outcome);

				executions.ShouldBe(1);
				release.SetResult();
				await operation.Match(new object());
				await operation.Match(new object());
				executions.ShouldBe(1);
			}
	}