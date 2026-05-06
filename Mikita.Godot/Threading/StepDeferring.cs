using Mikita.Steps;

namespace Mikita.Godot.Threading;

public static class StepDeferring
	{
		extension(IAsyncStep step)
			{
				public AsyncStep Deferred()
					=> new
						(
							@do: () => step.Defer(x => x.Do()),
							undo: () => step.Defer(x => x.Undo())
						);
			}
	}