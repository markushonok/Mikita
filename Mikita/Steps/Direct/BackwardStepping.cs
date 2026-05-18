using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Direct;

public static class BackwardStepping
	{
		extension(Step)
			{
				public static IAsyncStep Backward
					(
						Func<Task> undo
					)
					=> new AsyncStep
						(
							@do: () => Task.CompletedTask,
							undo
						);

				public static IStep Backward
					(
						Action undo
					)
					=> new Step
						(
							@do: delegate {},
							undo
						);
			}

		extension(IStep step)
			{
				public IStep AsBackward
					=> new Step
						(
							@do: delegate {},
							step.Undo
						);
			}
	}