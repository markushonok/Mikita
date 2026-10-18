using Mikita.Structs.Referring;
using Mikita.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Runs a step as a coalescing cycle: one operation at a time,
/// driven by the latest declaration.
/// </summary>
/// <remarks>
/// The contract, including call order, failure handling, and threading,
/// is in <c>Coalescing.md</c>.
/// </remarks>
public sealed class CoalescingStep
	(
		IAsyncStep source,
		IRef<StepState> state,
		IRef<Exception?> failure,
		IRef<TaskCompletionSource?> application,
		IRef<TaskCompletionSource?> release,
		Lock access
	)
	: IAsyncStep
	{
		public Task Do()
			=> Declare(isWorking: true);

		public Task Undo()
			=> Declare(isWorking: false);

		private Task Declare(bool isWorking)
			{
				TaskCompletionSource completion;
				bool isStarting;
				lock (access)
					{
						if (state.Value.IsFailed) return Report(isWorking);

						if (isWorking && state.Value.IsDeclaredWorking)
							throw RepeatedDo;

						var wasApplied = state.Value.IsApplied;
						isStarting
							= application.Value is null
								&& release.Value is null;
						state.SetTo(StateOf(isWorking, wasApplied));
						if (isStarting && isWorking == wasApplied)
							return Task.CompletedTask;

						var pending = isWorking ? application : release;
						completion = pending.Value ?? NewSettlement;
						pending.SetTo(completion);
					}
				if (isStarting) Perform().Forget();
				return completion.Task;
			}

		private Task Report(bool isWorking)
			=> isWorking || state.Value is StepState.FailedHeld
				? Task.FromException(Failure)
				: Task.CompletedTask;

		private Exception Failure
			=> failure.Value ?? UnreportedFailure;

		private async Task Perform()
			{
				while (TryContinue(out var isWorking))
					{
						try
							{
								await Apply(isWorking);
							}
						catch (Exception exception)
							{
								Break(isWorking, exception);
								return;
							}
					}
			}

		private bool TryContinue(out bool isWorking)
			{
				lock (access)
					{
						isWorking = state.Value.IsDeclaredWorking;
						if (isWorking != state.Value.IsApplied) return true;
						Settle(executionFailure: null, releaseFailure: null);
						return false;
					}
			}

		private async Task Apply(bool isWorking)
			{
				if (isWorking) await source.Do();
				else await source.Undo();
				lock (access)
					state.SetTo(StateOf(state.Value.IsDeclaredWorking, isWorking));
			}

		private void Break(bool isWorking, Exception exception)
			{
				lock (access)
					{
						if (exception is OperationCanceledException)
							{
								state.SetTo
									(
										StateOf(state.Value.IsApplied, state.Value.IsApplied)
									);
								Settle(exception, isWorking ? null : exception);
								return;
							}

						var cleanup = isWorking
							? exception is RollbackException rollback
								? new AggregateException(rollback.RollbackFailures)
								: null
							: exception;
						failure.SetTo(cleanup ?? exception);
						state.SetTo
							(
								isWorking && cleanup is null
									? StepState.FailedFree
									: StepState.FailedHeld
							);
						Settle(exception, cleanup);
					}
			}

		// Callers hold access: completing the cycle and accepting its next declaration
		// must not interleave.
		private void Settle(Exception? executionFailure, Exception? releaseFailure)
			{
				var applying = application.Value;
				var releasing = release.Value;
				application.SetTo(null);
				release.SetTo(null);
				Complete(applying, executionFailure);
				Complete(releasing, releaseFailure);
			}

		private static void Complete
			(
				TaskCompletionSource? completion,
				Exception? failure
			)
			{
				if (completion is null) return;
				if (failure is OperationCanceledException cancelled)
					completion.SetCanceled(cancelled.CancellationToken);
				else if (failure is not null) completion.SetException(failure);
				else completion.SetResult();
			}

		private static StepState StateOf(bool isDeclaredWorking, bool isApplied)
			=> (isDeclaredWorking, isApplied) switch
				{
					(true, true) => StepState.Done,
					(true, false) => StepState.Doing,
					(false, true) => StepState.Undoing,
					(false, false) => StepState.Undone
				};

		private static InvalidOperationException RepeatedDo
			=> new("Do has already been declared. Declare Undo before another Do.");

		private static InvalidOperationException UnreportedFailure
			=> new("The step failed earlier and cannot run the source again.");

		private static TaskCompletionSource NewSettlement
			=> new
				(
					TaskCreationOptions
						.RunContinuationsAsynchronously
				);
	}