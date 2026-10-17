using Mikita.Structs.Referring;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Provides reusable cancellation through Undo.
/// </summary>
public static class StepCancellation
	{
		extension(Step)
			{
				/// <summary>
				/// Creates a cancellable cycle whose Do task owns execution
				/// failures. Undo waits for rollback and reports only cleanup and
				/// cancellation-callback failures. The owner must observe Do.
				/// Failed cleanup must be reported through RollbackException.
				/// </summary>
				public static IAsyncStep WithObservedExecution
					(
						Func<CancellationToken, IAsyncStep> inner
					)
					=> new CancellableStep
						(
							inner,
							Ref.To<CancellationTokenSource?>(null),
							current: Ref.To(Step.Idle),
							execution: Ref.To(Task.CompletedTask),
							reportsExecutionFailure: false
						);

				/// <inheritdoc cref="CancellableStep"/>
				public static IAsyncStep With
					(
						Func<CancellationToken, IAsyncStep> inner
					)
					=> new CancellableStep
						(
							inner,
							Ref.To<CancellationTokenSource?>(null),
							current: Ref.To(Step.Idle),
							execution: Ref.To(Task.CompletedTask)
						);
			}
	}