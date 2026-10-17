using Mikita.Routines;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>Provides standard step compositions.</summary>
public static class StepInstancing
	{
		extension(Step)
			{
				/// <summary>
				/// A step that performs and reverses nothing.
				/// </summary>
				public static IAsyncStep Idle
					=> Idle;

				/// <summary>
				/// Supplies the token to forward work, which must clean up its
				/// own partial effects on failure. Cleanup never receives it.
				/// </summary>
				public static IAsyncStep That
					(
						CancellableTask @do,
						Func<Task> undo,
						CancellationToken cancel
					)
					=> new AsyncStep(() => @do(cancel), undo);

				public static IAsyncStep That
					(
						CancellableTask @do,
						CancellableTask undo
					)
					=> Step.That(() => @do(), () => undo());

				public static IAsyncStep That
					(
						Func<Task> @do,
						Func<Task> undo
					)
					=> new AsyncStep(@do, undo);

				public static IStep That
					(
						Action @do,
						Action undo
					)
					=> new Step(@do, undo);
			}

		private static readonly IAsyncStep Idle
			= new AsyncStep
				(
					() => Task.CompletedTask,
					() => Task.CompletedTask
				);
	}