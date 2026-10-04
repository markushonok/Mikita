using Mikita.Steps;
using Mikita.Steps.Direct;
using Mikita.Steps.Marching;
using Mikita.Steps.Walking;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Mikita.Test.Steps;

public static class CombinedStepTest
	{
		[Fact]
		public static async Task TakesForwardFromOneStepAndBackwardFromAnother()
			{
				var forward = new RecordingStep();
				var backward = new RecordingStep();
				var step = Step.From(forward, backward);

				await step.Do();
				await step.Undo();

				forward.DoCount.ShouldBe(1);
				forward.UndoCount.ShouldBe(0);
				backward.DoCount.ShouldBe(0);
				backward.UndoCount.ShouldBe(1);
			}

		[Fact]
		public static async Task PropagatesForwardFailureAfterItsOwnCompensation()
			{
				var performed = new RecordingStep();
				var failing = new FailingStep();
				var backward = new RecordingStep();
				var step = Step.From
					(
						March.Of([performed, failing]),
						backward
					);

				await Should.ThrowAsync<ExpectedException>
					(
						() => step.Do()
					);

				performed.UndoCount.ShouldBe(1);
				backward.UndoCount.ShouldBe(0);
			}

		[Fact]
		public static async Task PropagatesBackwardFailureAfterItsOwnCompensation()
			{
				var failing = new RecordingStep { FailUndo = true };
				var performed = new RecordingStep();
				var step = Step.From
					(
						new RecordingStep(),
						Walk.Of([failing, performed])
					);

				await Should.ThrowAsync<ExpectedException>
					(
						() => step.Undo()
					);

				performed.DoCount.ShouldBe(1);
			}

		[Fact]
		public static async Task StartsForwardConcurrentlyAndReversesBackwardInOrder()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var started = new ConcurrentQueue<int>();
				var undone = new ConcurrentQueue<int>();
				var steps = Controlled(doGates, undoGates, started, undone);
				var step = Step.From(March.Of(steps), Walk.Of(steps));

				var perform = step.Do();

				started.Order().ShouldBe([0, 1, 2]);

				Complete(doGates);
				await perform;

				var release = step.Undo();

				undone.ShouldBe([2]);

				Complete(undoGates);
				await release;

				undone.ShouldBe([2, 1, 0]);
			}

		private static IAsyncStep[] Controlled
			(
				TaskCompletionSource[] doGates,
				TaskCompletionSource[] undoGates,
				ConcurrentQueue<int> started,
				ConcurrentQueue<int> undone
			)
			=> Enumerable.Range(0, doGates.Length)
				.Select
					(
						id => new ControlledStep
							(
								id,
								doGates[id].Task,
								undoGates[id].Task,
								started,
								undone
							)
					)
				.ToArray();

		private static TaskCompletionSource[] Gates(int count)
			=> Enumerable.Range(0, count)
				.Select
					(
						_ => new TaskCompletionSource
							(
								TaskCreationOptions
									.RunContinuationsAsynchronously
							)
					)
				.ToArray();

		private static void Complete(TaskCompletionSource[] gates)
			{
				foreach (var gate in gates) gate.SetResult();
			}

		private sealed class RecordingStep: IAsyncStep
			{
				public int DoCount { get; private set; }
				public int UndoCount { get; private set; }
				public bool FailUndo { get; init; }

				public Task Do()
					{
						DoCount++;
						return Task.CompletedTask;
					}

				public Task Undo()
					{
						UndoCount++;
						return FailUndo
							? Task.FromException(new ExpectedException())
							: Task.CompletedTask;
					}
			}

		private sealed class FailingStep: IAsyncStep
			{
				public Task Do()
					=> Task.FromException(new ExpectedException());

				public Task Undo()
					=> Task.CompletedTask;
			}

		private sealed class ControlledStep
			(
				int id,
				Task doGate,
				Task undoGate,
				ConcurrentQueue<int> started,
				ConcurrentQueue<int> undone
			)
			: IAsyncStep
			{
				public async Task Do()
					{
						started.Enqueue(id);
						await doGate;
					}

				public async Task Undo()
					{
						undone.Enqueue(id);
						await undoGate;
					}
			}

		private sealed class ExpectedException: Exception;
	}