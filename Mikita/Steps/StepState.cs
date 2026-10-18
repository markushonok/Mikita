namespace Mikita.Steps;

/// <summary>
/// The phase of a coalescing cycle: the latest declaration, whether the
/// source reflects it, and what a failure left behind.
/// </summary>
/// <remarks>
/// The phases and their transitions are described in <c>Coalescing.md</c>.
/// </remarks>
public enum StepState
	{
		Undone,
		Doing,
		Done,
		Undoing,
		FailedFree,
		FailedHeld
	}