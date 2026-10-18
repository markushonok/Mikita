using Mikita.Steps;

namespace Mikita.Test.Steps;

public static class StepCoalescingTest
	{
		[Fact]
		public static async Task RepeatedDoIsRejected()
			{
				var adopted = 0;
				var step = Coalesced
					(
						adopt: () =>
							{
								adopted++;
								return Task.CompletedTask;
							},
						release: () => Task.CompletedTask
					);

				await step.Do();
				Should.Throw<InvalidOperationException>(() => step.Do());

				adopted.ShouldBe(1);
				await step.Undo();
				await step.Do();
				adopted.ShouldBe(2);
				await step.Undo();
			}

		[Fact]
		public static async Task UndoBeforeDoDoesNothing()
			{
				var released = 0;
				var step = Coalesced
					(
						adopt: () => Task.CompletedTask,
						release: () =>
							{
								released++;
								return Task.CompletedTask;
							}
					);

				await step.Undo();

				released.ShouldBe(0);
			}

		[Fact]
		public static async Task UndoWaitsForRunningDo()
			{
				var events = new List<string>();
				var entered = NewCompletion;
				var leave = NewCompletion;
				var step = Coalesced
					(
						adopt: () =>
							{
								events.Add("adopt");
								entered.SetResult();
								return leave.Task;
							},
						release: () =>
							{
								events.Add("release");
								return Task.CompletedTask;
							}
					);

				var applying = step.Do();
				await entered.Task;
				var releasing = step.Undo();

				events.ShouldBe(["adopt"]);

				leave.SetResult();
				await applying;

				events.ShouldBe(["adopt", "release"]);
				await releasing;
			}

		[Fact]
		public static async Task DoDuringDoIsRejected()
			{
				var events = new List<string>();
				var entered = NewCompletion;
				var leave = NewCompletion;
				var step = Coalesced
					(
						adopt: () =>
							{
								events.Add("adopt");
								entered.SetResult();
								return leave.Task;
							},
						release: () =>
							{
								events.Add("release");
								return Task.CompletedTask;
							}
					);

				var applying = step.Do();
				await entered.Task;
				Should.Throw<InvalidOperationException>(() => step.Do());
				var releasing = step.Undo();

				leave.SetResult();
				await applying;
				await releasing;

				events.ShouldBe(["adopt", "release"]);
			}

		[Fact]
		public static async Task OppositePendingDeclarationsCancelOut()
			{
				var events = new List<string>();
				var entered = NewCompletion;
				var leave = NewCompletion;
				var step = Coalesced
					(
						adopt: () =>
							{
								events.Add("adopt");
								entered.SetResult();
								return leave.Task;
							},
						release: () =>
							{
								events.Add("release");
								return Task.CompletedTask;
							}
					);

				var applying = step.Do();
				await entered.Task;
				var releasing = step.Undo();
				var reapplying = step.Do();
				reapplying.IsCompleted.ShouldBeFalse();

				leave.SetResult();
				await applying;
				await releasing;
				await reapplying;

				events.ShouldBe(["adopt"]);
			}

		[Fact]
		public static async Task DoWaitsForRunningUndo()
			{
				var release = NewCompletion;
				var applications = 0;
				var step = Coalesced
					(
						adopt: () =>
							{
								applications++;
								return Task.CompletedTask;
							},
						release: () => release.Task
					);
				await step.Do();
				var releasing = step.Undo();
				var applying = step.Do();
				applications.ShouldBe(1);
				applying.IsCompleted.ShouldBeFalse();

				release.SetResult();
				await Task.WhenAll(releasing, applying)
					.WaitAsync(TimeSpan.FromSeconds(5));
				applications.ShouldBe(2);
			}

		[Fact]
		public static async Task FailedDoBlocksTheStep()
			{
				var failure = new InvalidOperationException("acquire");
				var released = 0;
				var isFailing = true;
				var step = Coalesced
					(
						adopt: () => isFailing
							? Task.FromException(failure)
							: Task.CompletedTask,
						release: () =>
							{
								released++;
								return Task.CompletedTask;
							}
					);
				var applying = step.Do();
				var releasing = step.Undo();
				var observed = await Should.ThrowAsync<InvalidOperationException>
					(
						() => applying
					);
				observed.ShouldBeSameAs(failure);

				await releasing;
				released.ShouldBe(0);

				var repeated = await Should.ThrowAsync<InvalidOperationException>
					(step.Do);
				repeated.ShouldBeSameAs(failure);
			}

		[Fact]
		public static async Task FailedDoRollbackBlocksBothDirections()
			{
				var acquisition = NewCompletion;
				var cleanup = new InvalidOperationException("cleanup");
				var step = Coalesced
					(
						adopt: () => acquisition.Task,
						release: () => throw new InvalidOperationException("already undone")
					);
				var applying = step.Do();
				var releasing = step.Undo();
				acquisition.SetException
					(
						new RollbackException
							(
								new InvalidOperationException("acquire"),
								[cleanup]
							)
					);

				await Should.ThrowAsync<RollbackException>(() => applying);
				var failure = await Should.ThrowAsync<AggregateException>
					(
						() => releasing
					);
				failure.InnerExceptions.ShouldBe([cleanup]);

				await Should.ThrowAsync<AggregateException>(step.Do);
				await Should.ThrowAsync<AggregateException>(step.Undo);
			}

		[Fact]
		public static async Task FailedUndoBlocksCleanup()
			{
				var cleanup = new InvalidOperationException("cleanup");
				var releases = 0;
				var step = Coalesced
					(
						adopt: () => Task.CompletedTask,
						release: () =>
							{
								releases++;
								return Task.FromException(cleanup);
							}
					);
				await step.Do();
				var observed = await Should.ThrowAsync<InvalidOperationException>
					(step.Undo);
				observed.ShouldBeSameAs(cleanup);
				releases.ShouldBe(1);

				await Should.ThrowAsync<InvalidOperationException>(step.Undo);
				await Should.ThrowAsync<InvalidOperationException>(step.Do);
				releases.ShouldBe(1);
			}

		[Fact]
		public static async Task CleanupFailureReachesBothDirections()
			{
				var release = NewCompletion;
				var step = Coalesced
					(
						adopt: () => Task.CompletedTask,
						release: () => release.Task
					);
				await step.Do();
				var releasing = step.Undo();
				var applying = step.Do();
				release.SetException(new InvalidOperationException("cleanup"));

				await Should.ThrowAsync<InvalidOperationException>(() => releasing);
				await Should.ThrowAsync<InvalidOperationException>(() => applying);
			}

		[Fact]
		public static async Task CancelledDoDoesNotBlockTheStep()
			{
				var acquisition = NewCompletion;
				var applications = 0;
				var step = Coalesced
					(
						adopt: () =>
							{
								applications++;
								return applications == 1
									? acquisition.Task
									: Task.CompletedTask;
							},
						release: () => Task.CompletedTask
					);
				var applying = step.Do();
				acquisition.SetCanceled();

				await Should.ThrowAsync<OperationCanceledException>(() => applying);
				applying.IsCanceled.ShouldBeTrue();

				await step.Do();
				applications.ShouldBe(2);
				await step.Undo();
			}

		[Fact]
		public static async Task RepeatedUndoReleasesOnce()
			{
				var release = NewCompletion;
				var releases = 0;
				var step = Coalesced
					(
						adopt: () => Task.CompletedTask,
						release: () =>
							{
								releases++;
								return release.Task;
							}
					);
				await step.Do();
				var releasing = step.Undo();
				var repeated = step.Undo();
				releases.ShouldBe(1);

				release.SetResult();
				await Task.WhenAll(releasing, repeated)
					.WaitAsync(TimeSpan.FromSeconds(5));
				await step.Undo();
				releases.ShouldBe(1);
			}

		[Fact]
		public static async Task ConcurrentUndoAtCompletionIsNotLost()
			{
				for (var attempt = 0; attempt < 500; attempt++)
					{
						var acquisition = NewCompletion;
						var isApplied = false;
						var step = new FakeStep
							(
								adopt: async () =>
									{
										await acquisition.Task;
										isApplied = true;
									},
								release: () =>
									{
										isApplied = false;
										return Task.CompletedTask;
									}
							)
							.WithCoalescing;
						var applying = step.Do();
						var releasing = Task.Run(step.Undo);
						acquisition.SetResult();

						await Task
							.WhenAll(applying, releasing)
							.WaitAsync(TimeSpan.FromSeconds(5));
						isApplied.ShouldBeFalse();
					}
			}

		private static IAsyncStep Coalesced(Func<Task> adopt, Func<Task> release)
			=> new FakeStep(adopt, release).WithCoalescing;

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);

		private sealed class FakeStep(Func<Task> adopt, Func<Task> release)
			: IAsyncStep
			{
				public Task Do() => adopt();
				public Task Undo() => release();
			}
	}