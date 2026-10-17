using Mikita.Steps;
using Mikita.Steps.Direct;
using Mikita.Steps.Marching;
using Mikita.Steps.Walking;

namespace Mikita.Test.Steps;

public static class StepFailureObservationTest
	{
		[Fact]
		public static async Task ExecutionAggregateIsNotCleanupFailure()
			{
				var failure = new AggregateException
					(new InvalidOperationException("execution"));
				var step = Step.WithObservedExecution
					(
						cancel => Step.Forward(() => Task.FromException(failure))
					);
				var observed = await Should.ThrowAsync<AggregateException>(step.Do);
				observed.ShouldBeSameAs(failure);
				await step.Undo();
			}

		[Fact]
		public static async Task UndoReportsOnlyFailedRollback()
			{
				var execution = new InvalidOperationException("execution");
				var cleanup = new InvalidOperationException("cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => Walk.Of
							(
								[
									Step.That
										(
											() => Task.CompletedTask,
											() => Task.FromException(cleanup)
										),
									Step.Forward(() => Task.FromException(execution))
								],
								cancel
							)
					);
				var failure = await Should.ThrowAsync<RollbackException>(step.Do);
				failure.ExecutionFailure.ShouldBeSameAs(execution);
				failure.RollbackFailures.ShouldBe([cleanup]);
				var cleanupFailure = await Should.ThrowAsync<AggregateException>
					(step.Undo);
				cleanupFailure.InnerExceptions.ShouldBe([cleanup]);
			}

		[Fact]
		public static async Task NestedRollbackFailuresRemainSeparate()
			{
				var execution = new InvalidOperationException("execution");
				var inner = new InvalidOperationException("inner cleanup");
				var outer = new InvalidOperationException("outer cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => Walk.Of
							(
								[
									Step.That
										(
											() => Task.CompletedTask,
											() => Task.FromException(outer)
										),
									Walk.Of
										(
											[
												Step.That
													(
														() => Task.CompletedTask,
														() => Task.FromException(inner)
													),
												Step.Forward(() => Task.FromException(execution))
											],
											cancel
										)
								],
								cancel
							)
					);
				var failure = await Should.ThrowAsync<RollbackException>(step.Do);
				failure.ExecutionFailure.ShouldBeSameAs(execution);
				failure.RollbackFailures.ShouldBe([inner, outer]);
				var cleanupFailure = await Should.ThrowAsync<AggregateException>
					(step.Undo);
				cleanupFailure.Flatten().InnerExceptions.ShouldBe([inner, outer]);
			}

		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public static async Task CallbackFailureRemainsObservable
			(
				bool reportsExecutionFailure
			)
			{
				var execution = new InvalidOperationException("execution");
				var callback = new InvalidOperationException("callback");
				IAsyncStep Factory(CancellationToken cancel)
					{
						cancel.Register(() => throw callback);
						return Step.Forward(() => Task.FromException(execution));
					}
				var step = reportsExecutionFailure
					? Step.With(Factory)
					: Step.WithObservedExecution(Factory);
				await Should.ThrowAsync<InvalidOperationException>(step.Do);
				var failure = await Should.ThrowAsync<AggregateException>(step.Undo);
				failure.Flatten().InnerExceptions.ShouldBe
					(
						reportsExecutionFailure
							? [execution, callback]
							: [callback],
						ignoreOrder: true
					);
			}

		[Fact]
		public static async Task SuccessfulExecutionStillRequiresCleanup()
			{
				var cleanup = new InvalidOperationException("cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => Step.That
							(
								() => Task.CompletedTask,
								() => Task.FromException(cleanup)
							)
					);
				await step.Do();
				var failure = await Should.ThrowAsync<InvalidOperationException>
					(step.Undo);
				failure.ShouldBeSameAs(cleanup);
			}

		[Fact]
		public static async Task FactoryFailureBelongsToExecution()
			{
				var execution = new InvalidOperationException("factory");
				var step = Step.WithObservedExecution(cancel => throw execution);
				var running = step.Do();
				var failure = await Should.ThrowAsync<InvalidOperationException>
					(() => running);
				failure.ShouldBeSameAs(execution);
				await step.Undo();
			}

		[Fact]
		public static async Task CancellationStillReportsFailedRollback()
			{
				var pending = new TaskCompletionSource
					(TaskCreationOptions.RunContinuationsAsynchronously);
				var cleanup = new InvalidOperationException("cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => Walk.Of
							(
								[
									Step.That
										(
											() => pending.Task,
											() => Task.FromException(cleanup)
										)
								],
								cancel
							)
					);
				var execution = step.Do();
				var release = step.Undo();
				pending.SetResult();
				await Should.ThrowAsync<RollbackException>(() => execution);
				var cleanupFailure = await Should.ThrowAsync<AggregateException>
					(() => release);
				cleanupFailure.InnerExceptions.ShouldBe([cleanup]);
			}

		[Fact]
		public static async Task MarchKeepsNestedRollbackFailuresSeparate()
			{
				var execution = new InvalidOperationException("execution");
				var parallel = new InvalidOperationException("parallel execution");
				var inner = new InvalidOperationException("inner cleanup");
				var outer = new InvalidOperationException("outer cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => March.Of
							(
								[
									Walk.Of
										(
											[
												Step.That
													(
														() => Task.CompletedTask,
														() => Task.FromException(inner)
													),
												Step.Forward(() => Task.FromException(execution))
											],
											cancel
										),
									Step.Forward(() => Task.FromException(parallel)),
									Step.That
										(
											() => Task.CompletedTask,
											() => Task.FromException(outer)
										)
								]
							)
					);
				var failure = await Should.ThrowAsync<RollbackException>(step.Do);
				failure.RollbackFailures.ShouldBe([inner, outer]);
				var executionFailure = failure.ExecutionFailure
					.ShouldBeOfType<AggregateException>();
				executionFailure.InnerExceptions.ShouldBe([execution, parallel]);
				var cleanupFailure = await Should.ThrowAsync<AggregateException>
					(step.Undo);
				cleanupFailure.Flatten().InnerExceptions.ShouldBe([inner, outer]);
			}

		[Fact]
		public static async Task MarchExecutionFailuresDoNotFailCleanup()
			{
				var first = new InvalidOperationException("first");
				var second = new InvalidOperationException("second");
				var step = Step.WithObservedExecution
					(
						cancel => March.Of
							(
								[
									Step.Forward(() => Task.FromException(first)),
									Step.Forward(() => Task.FromException(second))
								]
							)
					);
				await Should.ThrowAsync<AggregateException>(step.Do);
				await step.Undo();
			}

		[Fact]
		public static async Task CallbackFailureDoesNotSkipCleanup()
			{
				var callback = new InvalidOperationException("callback");
				var cleanup = new InvalidOperationException("cleanup");
				var step = Step.WithObservedExecution
					(
						cancel =>
							{
								cancel.Register(() => throw callback);
								return Step.That
									(
										() => Task.CompletedTask,
										() => Task.FromException(cleanup)
									);
							}
					);
				await step.Do();
				var failure = await Should.ThrowAsync<AggregateException>(step.Undo);
				failure.Flatten().InnerExceptions.ShouldBe
					([cleanup, callback], ignoreOrder: true);
			}

		[Fact]
		public static async Task LeafCanReportPartialCleanupFailure()
			{
				var execution = new InvalidOperationException("execution");
				var cleanup = new InvalidOperationException("cleanup");
				var step = Step.WithObservedExecution
					(
						cancel => Step.Forward
							(
								() => Task.FromException
									(
										new RollbackException(execution, [cleanup])
									)
							)
					);
				await Should.ThrowAsync<RollbackException>(step.Do);
				var cleanupFailure = await Should.ThrowAsync<AggregateException>(step.Undo);
				cleanupFailure.InnerExceptions.ShouldBe([cleanup]);
			}
	}