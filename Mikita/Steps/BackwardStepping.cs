using Mikita.Routines;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class BackwardStepping
	{
		extension(Step step)
			{
				public static Step Backward
					(
						Action undo
					)
					=> new
						(
							@do: delegate {},
							undo
						);

				public Step AsBackward
					=> new
						(
							@do: delegate {},
							step.Undo
						);
			}
	}