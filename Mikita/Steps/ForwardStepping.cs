using Mikita.Routines;
using System;
using System.Threading.Tasks;

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

				public static AsyncStep Forward
					(
						CancellableTask @do
					)
					=> new
						(
							@do,
							undo: cancellation => Task.CompletedTask
						);

				public Step AsForward
					=> new
						(
							step.Do,
							undo: delegate {}
						);
			}
	}