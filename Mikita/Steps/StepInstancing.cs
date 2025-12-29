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
						CancellableTask @do,
						CancellableTask undo
					)
					=> new(@do, undo);
			}
	}