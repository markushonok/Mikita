using Mikita.Steps;
using Mikita.Steps.Marching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikita.Test.Steps;

public static class AsyncMarchTest
	{
		[Fact]
		public static async Task StartsEveryStepBeforeAnyCompletes()
			{
				var doGates = Gates(3);
				var started = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps(doGates, started)
					);

				var perform = march.Do();

				started.ToArray().ShouldBe([0, 1, 2]);

				Complete(doGates);
				await perform;
			}

		[Fact]
		public static async Task UndoesEveryStepBeforeAnyReversalCompletes()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var undone = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								undone: undone
							)
					);

				var perform = march.Do();
				Complete(doGates);
				await perform;

				var release = march.Undo();

				undone.Order().ShouldBe([0, 1, 2]);

				Complete(undoGates);
				await release;
			}

		[Fact]
		public static async Task PerformsAndReversesEveryStep()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var performed = new ConcurrentQueue<int>();
				var reversed = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								performed: performed,
								reversed: reversed
							)
					);

				var perform = march.Do();
				Complete(doGates);
				await perform;

				var release = march.Undo();
				Complete(undoGates);
				await release;

				performed.Order().ShouldBe([0, 1, 2]);
				reversed.Order().ShouldBe([0, 1, 2]);
			}

		[Fact]
		public static async Task UndoesPerformedStepsWhenOneFails()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var undone = new ConcurrentQueue<int>();
				var reversed = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								undone: undone,
								reversed: reversed
							)
					);

				var perform = march.Do();
				doGates[0].SetResult();
				doGates[1].SetResult();
				doGates[2].SetException(new ExpectedException());
				undoGates[0].SetResult();
				undoGates[1].SetResult();

				await Should.ThrowAsync<ExpectedException>
					(
						() => perform
					);

				undone.Order().ShouldBe([0, 1]);
				reversed.Order().ShouldBe([0, 1]);
			}

		[Fact]
		public static async Task AwaitsEveryStartedStepBeforeCompensatingWhenOneFails()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var undone = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								undone: undone
							)
					);

				var perform = march.Do();
				doGates[0].SetResult();
				doGates[2].SetException(new ExpectedException());

				undone.ShouldBeEmpty();
				perform.IsCompleted.ShouldBeFalse();

				doGates[1].SetResult();
				undoGates[0].SetResult();
				undoGates[1].SetResult();

				await Should.ThrowAsync<ExpectedException>
					(
						() => perform
					);

				undone.Order().ShouldBe([0, 1]);
			}

		[Fact]
		public static async Task ReportsOperationAndCompensationFailuresTogether()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var performed = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								performed: performed
							)
					);

				var perform = march.Do();
				doGates[0].SetResult();
				doGates[1].SetResult();
				doGates[2].SetException(new ExpectedException());
				undoGates[0].SetResult();
				undoGates[1].SetException(new ExpectedException());

				var aggregate = await Should.ThrowAsync<AggregateException>
					(
						() => perform
					);

				aggregate.InnerExceptions.Count.ShouldBe(2);
				aggregate.InnerExceptions.ShouldAllBe(x => x is ExpectedException);
			}

		[Fact]
		public static async Task ReversesPerformedStepsWhenOneUndoFails()
			{
				var doGates = Gates(3);
				var undoGates = Gates(3);
				var performed = new ConcurrentQueue<int>();
				var reversed = new ConcurrentQueue<int>();
				var march = March.Of
					(
						Steps
							(
								doGates,
								undoGates: undoGates,
								performed: performed,
								reversed: reversed
							)
					);

				var perform = march.Do();
				Complete(doGates);
				await perform;

				var release = march.Undo();
				undoGates[0].SetResult();
				undoGates[1].SetResult();
				undoGates[2].SetException(new ExpectedException());

				await Should.ThrowAsync<ExpectedException>
					(
						() => release
					);

				performed.Count(x => x is 0).ShouldBe(2);
				performed.Count(x => x is 1).ShouldBe(2);
				performed.Count(x => x is 2).ShouldBe(1);
				reversed.Order().ShouldBe([0, 1]);
			}

		private static IAsyncStep[] Steps
			(
				TaskCompletionSource[] doGates,
				ConcurrentQueue<int>? started = null,
				TaskCompletionSource[]? undoGates = null,
				ConcurrentQueue<int>? performed = null,
				ConcurrentQueue<int>? undone = null,
				ConcurrentQueue<int>? reversed = null
			)
			=> Enumerable.Range(0, doGates.Length)
				.Select(id => new ControlledStep
					(
						id,
						doGates[id].Task,
						undoGates?[id].Task ?? Task.CompletedTask,
						started ?? new ConcurrentQueue<int>(),
						performed ?? new ConcurrentQueue<int>(),
						undone ?? new ConcurrentQueue<int>(),
						reversed ?? new ConcurrentQueue<int>()
					))
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

		private sealed class ControlledStep
			(
				int id,
				Task doGate,
				Task undoGate,
				ConcurrentQueue<int> started,
				ConcurrentQueue<int> performed,
				ConcurrentQueue<int> undone,
				ConcurrentQueue<int> reversed
			)
			: IAsyncStep
			{
				public async Task Do()
					{
						started.Enqueue(id);
						await doGate;
						performed.Enqueue(id);
					}

				public async Task Undo()
					{
						undone.Enqueue(id);
						await undoGate;
						reversed.Enqueue(id);
					}
			}

		private sealed class ExpectedException: Exception;
	}