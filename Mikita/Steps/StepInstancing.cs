using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepInstancing
	{
		extension(Step)
			{
				public static IStep Set<T>
					(
						IRef<T> reference,
						Func<T> initialize,
						T deinitialize
					)
					=> Step.That
						(
							@do: () => reference.SetTo(initialize()),
							undo: () => reference.SetTo(deinitialize)
						);

				public static Step That
					(
						Action @do,
						Action undo
					)
					=> new(@do, undo);

				public static AsyncStep That
					(
						Func<Task> @do,
						Func<Task> undo
					)
					=> new(@do, undo);
			}
	}