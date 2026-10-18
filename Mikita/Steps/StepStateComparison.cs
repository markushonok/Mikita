namespace Mikita.Steps;

/// <summary>
/// Provides step-state predicates.
/// </summary>
public static class StepStateComparison
	{
		extension(StepState state)
			{
				/// <summary>Whether the source is applied or may have
				/// remained applied.</summary>
				public bool IsApplied
					=> state
						is StepState.Done
						or StepState.Undoing
						or StepState.FailedHeld;

				/// <summary>Whether the latest declaration asks for
				/// work.</summary>
				public bool IsDeclaredWorking
					=> state is StepState.Doing or StepState.Done;

				/// <summary>Whether a failure ended the cycle.</summary>
				public bool IsFailed
					=> state
						is StepState.FailedFree
						or StepState.FailedHeld;
			}
	}