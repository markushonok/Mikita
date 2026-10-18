using Mikita.Structs.Referring;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Provides step coalescing.
/// </summary>
public static class StepCoalescing
	{
		extension(IAsyncStep step)
			{
				/// <summary>Coalesces the step's calls; see
				/// <see cref="CoalescingStep"/>.</summary>
				public IAsyncStep WithCoalescing
					=> new CoalescingStep
						(
							step,
							state: Ref.To(StepState.Undone),
							failure: Ref<Exception>.Null,
							application: Ref<TaskCompletionSource>.Null,
							release: Ref<TaskCompletionSource>.Null,
							new Lock()
						);
			}
	}