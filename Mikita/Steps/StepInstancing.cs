using Mikita.Routines;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepInstancing
	{
		extension(Step)
			{
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
	}