namespace Mikita.Steps.Direct;

/// <summary>
/// Provides a step that takes its forward direction from one step and
/// its backward direction from another.
/// </summary>
/// <remarks>
/// The forward step supplies only its <see cref="IAsyncStep.Do"/> and
/// the backward step only its <see cref="IAsyncStep.Undo"/>. Each keeps
/// its own compensation; the surrounding composition covers the
/// cross-step one.
/// </remarks>
public static class CombinedStepping
	{
		extension(Step)
			{
				public static IAsyncStep From
					(
						IAsyncStep forward,
						IAsyncStep backward
					)
					=> Step.That(forward.Do, backward.Undo);
			}
	}