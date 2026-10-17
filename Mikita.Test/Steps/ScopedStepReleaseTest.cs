using Mikita.Steps;
using Mikita.Steps.Direct;
using Mikita.Steps.Scopes;
using Mikita.Steps.Walking;

namespace Mikita.Test.Steps;

public static class ScopedStepReleaseTest
	{
		[Fact]
		public static async Task LateAcquisitionIsReleased()
			{
				var pending = new TaskCompletionSource<int>
					(
						TaskCreationOptions.RunContinuationsAsynchronously
					);
				var released = 0;
				var observed = false;
				var step = Step.With
					(
						cancel => Step.NewScoped
							(
								initialize: cancel => pending.Task,
								release: value =>
									{
										released += value;
										return Task.CompletedTask;
									},
								pattern: value => Step.Forward(() => observed = true).AsAsync,
								cancel
							)
					);
				var execution = step.Do();
				var cleanup = step.Undo();
				cleanup.IsCompleted.ShouldBeFalse();
				pending.SetResult(1);
				await cleanup;
				released.ShouldBe(1);
				observed.ShouldBeFalse();
				execution.IsCanceled.ShouldBeTrue();
			}

		[Fact]
		public static async Task FailedPatternReleasesAcquisition()
			{
				var released = false;
				var step = Step.NewScoped
					(
						initialize: cancel => Task.FromResult(1),
						release: value =>
							{
								released = true;
								return Task.CompletedTask;
							},
						pattern: value => throw new InvalidOperationException("pattern"),
						CancellationToken.None
					);
				await Should.ThrowAsync<InvalidOperationException>(step.Do);
				released.ShouldBeTrue();
			}

		[Fact]
		public static async Task AcquisitionObservesCancellation()
			{
				var entered = NewCompletion;
				var released = false;
				var step = Walk.Of
					(
						cancel =>
							[
								Step.NewScoped
									(
										initialize: async token =>
											{
												entered.SetResult();
												await Task.Delay(Timeout.Infinite, token);
												return 1;
											},
										release: value =>
											{
												released = true;
												return Task.CompletedTask;
											},
										pattern: value => Step.Forward(delegate {}).AsAsync,
										cancel
									)
							]
					);
				var execution = step.Do();
				await entered.Task;
				await step.Undo();
				execution.IsCanceled.ShouldBeTrue();
				released.ShouldBeFalse();
			}

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);
	}