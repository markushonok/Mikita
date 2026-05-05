using System;

namespace Mikita.Steps;

public static class ForwardStepping
	{
		extension(Step step)
			{
				public static Step Forward
					(
						Action @do
					)
					=> new
						(
							@do,
							undo: delegate {}
						);

				public Step AsForward
					=> new
						(
							step.Do,
							undo: delegate {}
						);
			}
	}