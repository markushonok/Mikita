using Mikita.Steps;
using Mikita.Steps.Direct;
using Mikita.Steps.Walking;

namespace Mikita.Test.Steps;

public static class WalkCancellationTest
	{
		[Fact]
		public static async Task RollsBackCompletedSteps()
			{
				using var cancel = new CancellationTokenSource();
				var effects = new List<int>();
				var step = Walk.Of
					(
						[
							Step.That
								(
									() => effects.Add(1),
									() => effects.Remove(1)
								).AsAsync,
							Step.That(cancel.Cancel, () => effects.Add(2)).AsAsync,
							Step.Forward(() => effects.Add(3)).AsAsync
						],
						cancel.Token
					);
				await Should.ThrowAsync<OperationCanceledException>(step.Do);
				effects.ShouldBe([2]);
			}

		[Fact]
		public static async Task RollsBackAfterTheLastStep()
			{
				using var cancel = new CancellationTokenSource();
				var reversed = false;
				var step = Walk.Of
					(
						[Step.That(cancel.Cancel, () => reversed = true).AsAsync],
						cancel.Token
					);
				await Should.ThrowAsync<OperationCanceledException>(step.Do);
				reversed.ShouldBeTrue();
			}

		[Fact]
		public static async Task DoesNothingBeforeExecution()
			{
				using var cancel = new CancellationTokenSource();
				cancel.Cancel();
				var performed = false;
				var step = Walk.Of
					(
						[Step.Forward(() => performed = true).AsAsync],
						cancel.Token
					);
				await Should.ThrowAsync<OperationCanceledException>(step.Do);
				performed.ShouldBeFalse();
			}

		[Fact]
		public static async Task NestedWalkReceivesToken()
			{
				var entered = NewCompletion;
				var reversed = false;
				var step = Step.With
					(
						cancel => Walk.Of
							(
								[
									Walk.Of
										(
											[
												Step.That
													(
														delegate {},
														() => reversed = true
													).AsAsync,
												Step.That
													(
														async token =>
															{
																entered.SetResult();
																await Task.Delay
																	(
																		Timeout.Infinite,
																		token
																	);
															},
														() => Task.CompletedTask,
														cancel
													)
											],
											cancel
										)
								],
								cancel
							)
					);
				var execution = step.Do();
				await entered.Task;
				await step.Undo();
				execution.IsCanceled.ShouldBeTrue();
				reversed.ShouldBeTrue();
			}

		[Fact]
		public static async Task UndoStopsAtTheFirstFailure()
			{
				var acquired = 0;
				var released = new List<int>();
				var step = Walk.Of
					(
						cancel =>
							[
								Step.That
									(
										() => acquired++,
										() => released.Add(1)
									).AsAsync,
								Step.That
									(
										delegate {},
										() => throw new InvalidOperationException("cleanup")
									).AsAsync,
								Step.That
									(
										delegate {},
										() => released.Add(3)
									).AsAsync
							]
					);
				await step.Do();
				await Should.ThrowAsync<InvalidOperationException>(step.Undo);
				acquired.ShouldBe(1);
				released.ShouldBe([3]);
			}

		[Fact]
		public static async Task CallbackFailureDoesNotSkipRollback()
			{
				var released = false;
				var reached = NewCompletion;
				CancellationTokenRegistration registration = default;
				var step = Walk.Of
					(
						cancel =>
							[
								Step.That
									(
										() => registration = cancel.Register
											(
												() => throw new InvalidOperationException("callback")
											),
										() => { registration.Dispose(); released = true; }
									).AsAsync,
								Step.Forward
									(
										token =>
											{
												reached.SetResult();
												return Task.Delay(Timeout.Infinite, token);
											},
										cancel
									)
							]
					);
				var execution = step.Do();
				await reached.Task;
				try
					{
						await step.Undo();
					}
				catch (AggregateException)
					{
					}
				execution.IsCanceled.ShouldBeTrue();
				released.ShouldBeTrue();
			}

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);
	}