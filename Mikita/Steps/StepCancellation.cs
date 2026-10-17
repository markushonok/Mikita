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
				/// <inheritdoc cref="CancellableStep"/>
				public static IAsyncStep With
					(
						Func<CancellationToken, IAsyncStep> factory
					)
					=> new CancellableStep
						(
							factory,
							Ref.To<CancellationTokenSource?>(null),
							current: Ref.To(Step.Idle),
							execution: Ref.To(Task.CompletedTask)
						);
			}
	}