using Mikita.Routines;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps.Direct;

/// <summary>
/// Provides steps that perform an operation without reversing it.
/// </summary>
public static class ForwardStepping
	{
		extension(Step)
			{
				/// <summary>
				/// Supplies the token to forward work that reverses nothing; work
				/// that fails must clean up its own partial effects.
				/// </summary>
				public static IAsyncStep Forward
					(
						CancellableTask @do,
						CancellationToken cancel
					)
					=> new AsyncStep
						(
							() => @do(cancel),
							() => Task.CompletedTask
						);

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