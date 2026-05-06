using Mikita.Routines;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class AsyncForwardStepping
	{
		extension(Step)
			{
				public static AsyncStep Forward
					(
						CancellableTask @do
					)
					=> new
						(
							() => @do(),
							undo: () => Task.CompletedTask
						);

				public static AsyncStep Forward
					(
						Func<Task> @do
					)
					=> new
						(
							@do,
							undo: () => Task.CompletedTask
						);
			}
		
		extension(AsyncStep)
			{
				public static AsyncStep Forward
					(
						Action @do
					)
					=> Step.Forward(@do).AsAsync;
			}
	}