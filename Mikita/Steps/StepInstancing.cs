using Mikita.Routines;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepInstancing
	{
		extension(Step)
			{
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