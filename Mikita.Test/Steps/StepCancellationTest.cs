using Mikita.Steps;
using Mikita.Steps.Direct;
using Mikita.Steps.Walking;
using Mikita.Threading;
using System.Collections.Concurrent;

namespace Mikita.Test.Steps;

public static class StepCancellationTest
	{
		[Fact]
		public static async Task UndoWaitsForExecution()
			{
				var pending = NewCompletion;
				var reversed = new List<int>();
				var step = Step.With
					(
						cancel => Walk.Of
							(
								[
									Step.That
										(
											() => Task.CompletedTask,
											() =>
												{
													reversed.Add(1);
													return Task.CompletedTask;
												}
										),
									Step.That
										(
											() => pending.Task,
											() =>
												{
													reversed.Add(2);
													return Task.CompletedTask;
												}
										)
								],
								cancel
							)
					);
				var execution = step.Do();
				var cleanup = step.Undo();
				cleanup.IsCompleted.ShouldBeFalse();
				pending.SetResult();
				await cleanup;
				await Should.ThrowAsync<OperationCanceledException>
					(
						() => execution
					);
				execution.IsCanceled.ShouldBeTrue();
				reversed.ShouldBe([2, 1]);
			}

		[Fact]
		public static async Task UndoReportsFailure()
			{
				var step = Step.With
					(
						cancel => Step.Forward
							(
								() => Task.FromException
									(
										new InvalidOperationException("execution")
									)
							)
					);
				step.Do().Forget();
				var failure = await Should.ThrowAsync<InvalidOperationException>
					(
						step.Undo
					);
				failure.Message.ShouldBe("execution");
			}

		[Fact]
		public static async Task CancellationDoesNotHideRollbackFailure()
			{
				var pending = NewCompletion;
				var step = Step.With
					(
						cancel => Walk.Of
							(
								[
									Step.That
										(
											() => pending.Task,
											() => Task.FromException
												(
													new InvalidOperationException("cleanup")
												)
										)
								],
								cancel
							)
					);
				var execution = step.Do();
				var cleanup = step.Undo();
				pending.SetResult();
				var failure = await Should.ThrowAsync<AggregateException>
					(
						() => cleanup
					);
				failure.Flatten().InnerExceptions
					.ShouldContain(error => error.Message == "cleanup");
				execution.IsFaulted.ShouldBeTrue();
			}

		[Fact]
		public static async Task UndoReversesSuccessfulExecution()
			{
				var released = 0;
				var step = Step.With
					(
						cancel => Step.That
							(
								delegate {},
								() => released++
							).AsAsync
					);
				await step.Do();
				await step.Undo();
				released.ShouldBe(1);
			}

		[Fact]
		public static async Task RunsEveryCycle()
			{
				var released = new List<int>();
				var cycle = 0;
				var step = Step.With
					(
						cancel =>
							{
								var index = cycle++;
								return Step.That
									(
										delegate {},
										() => released.Add(index)
									).AsAsync;
							}
					);
				await step.Do();
				await step.Undo();
				await step.Do();
				await step.Undo();
				released.ShouldBe([0, 1]);
			}

		[Fact]
		public static async Task OverlappingCyclesAreIndependent()
			{
				var pending = new[] { NewCompletion, NewCompletion };
				var rolledBack = new ConcurrentQueue<int>();
				var cycle = 0;
				var step = Step.With
					(
						cancel =>
							{
								var index = cycle++;
								return Walk.Of
									(
										[
											Step.That
												(
													async () => await pending[index].Task,
													() =>
														{
															rolledBack.Enqueue(index);
															return Task.CompletedTask;
														}
												)
										],
										cancel
									);
							}
					);
				var first = step.Do();
				var firstUndo = step.Undo();
				var second = step.Do();
				var secondUndo = step.Undo();
				pending[0].SetResult();
				pending[1].SetResult();
				await Task.WhenAll(firstUndo, secondUndo);
				first.IsCanceled.ShouldBeTrue();
				second.IsCanceled.ShouldBeTrue();
				rolledBack.ShouldBe([0, 1], ignoreOrder: true);
			}

		[Fact]
		public static async Task UndoWithoutDoIsIdle()
			{
				var performed = false;
				var step = Step.With
					(
						cancel => Step.Forward(() => performed = true).AsAsync
					);
				await step.Undo();
				await step.Do();
				performed.ShouldBeTrue();
			}

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);
	}