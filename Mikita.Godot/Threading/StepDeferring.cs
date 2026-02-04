using Mikita.Steps;

namespace Mikita.Godot.Threading;

public static class StepDeferring
	{
		extension(IAsyncStep step)
			{
				public AsyncStep Deferred()
					=> new
						(
							@do: cancel => step.Defer(x => x.Do(cancel), cancel),
							undo: cancel => step.Defer(x => x.Undo(cancel), cancel)
						);
			}
	}