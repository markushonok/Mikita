using Mikita.Structs.Referring;
using Mikita.Threading;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Runs a step as a reusable cancellable cycle. Do starts a cycle; Undo may
/// overlap it, cancels that cycle, and awaits its rollback. Calls alternate
/// Do, Undo, Do, Undo; an Undo without a preceding Do does nothing.
/// </summary>
/// <remarks>
/// The factory receives the cycle's execution token; its step must roll back
/// partial execution on failure, as Walk does. Undo never receives that token,
/// suppresses expected execution cancellation, and surfaces real execution and
/// cleanup failures. With reportsExecutionFailure false, the owner must observe
/// Do: Undo reports only cleanup and cancellation-callback failures, and failed
/// rollback must be reported explicitly through RollbackException, including by
/// leaf steps. The API is not thread-safe: the caller serializes access
/// and issues calls in order. Each call captures its cycle, so a later Do may
/// replace the references before an earlier Undo finishes. Never synchronously
/// wait for Undo from work it must await.
/// </remarks>
public sealed class CancellableStep
	(
		Func<CancellationToken, IAsyncStep> inner,
		IRef<CancellationTokenSource?> cancelSource,
		IRef<IAsyncStep> current,
		IRef<Task> execution,
		bool reportsExecutionFailure = true
	)
	: IAsyncStep
	{
		public Task Do()
			{
				var newCancelSource = new CancellationTokenSource();
				cancelSource.SetTo(newCancelSource);

				var running = inner.DoWithThrowAsTask
					(
						factory =>
							{
								var step = factory(newCancelSource.Token);
								current.SetTo(step);
								return step.Do();
							}
					);
				execution.SetTo(running);
				return running;
			}

		public Task Undo()
			{
				if (cancelSource.Value is not {} cancelSourceValue)
					return Task.CompletedTask;

				return Release
					(
						cancelSourceValue,
						step: current.Value,
						running: execution.Value,
						reportsExecutionFailure
					);
			}

		private static async Task Release
			(
				CancellationTokenSource source,
				IAsyncStep step,
				Task running,
				bool reportsExecutionFailure
			)
			{
				try
					{
						var cancelFailure = await source.CancelAsync().WaitException;
						var executionFailure = await running.WaitException;

						var failure = executionFailure switch
							{
								null => await Cleanup(step),
								OperationCanceledException => null,
								_ when reportsExecutionFailure => executionFailure,
								RollbackException rollback => new AggregateException
									(
										rollback.RollbackFailures
									),
								_ => null
							};
						failure = Combined(failure, cancelFailure);

						if (failure is not null)
							ExceptionDispatchInfo.Capture(failure).Throw();
					}
				finally
					{
						source.Dispose();
					}
			}

		private static Task<Exception?> Cleanup(IAsyncStep step)
			=> step
				.DoWithThrowAsTask(static value => value.Undo())
				.WaitException;

		private static Exception? Combined
			(
				Exception? first,
				Exception? second
			)
			=> first is null || second is null
				? first ?? second
				: new AggregateException(first, second);
	}