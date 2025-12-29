using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepDesyncing
	{
		extension(IStep step)
			{
				public IAsyncStep AsAsync
					=> Step.That
						(
							@do: cancellation => Task.Run(step.Do, cancellation),
							undo: cancellation => Task.Run(step.Undo, cancellation)
						);
			}
	}