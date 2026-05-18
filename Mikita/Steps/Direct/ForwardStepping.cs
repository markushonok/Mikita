using Mikita.Routines;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Direct;

public static class ForwardStepping
	{
		extension(Step)
			{
				public static IAsyncStep Forward
					(
						CancellableTask @do
					)
					=> new AsyncStep
						(
							() => @do(),
							undo: () => Task.CompletedTask
						);

				public static IAsyncStep Forward
					(
						Func<Task> @do
					)
					=> new AsyncStep
						(
							@do,
							undo: () => Task.CompletedTask
						);

				public static IStep Forward
					(
						Action @do
					)
					=> new Step
						(
							@do,
							undo: delegate {}
						);
			}

		extension(IStep step)
			{
				public IStep AsForward
					=> new Step
						(
							step.Do,
							undo: delegate {}
						);
			}
	}