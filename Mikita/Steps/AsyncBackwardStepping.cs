using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class AsyncBackwardStepping
	{
		extension(Step)
			{
				public static AsyncStep Backward
					(
						Func<Task> undo
					)
					=> new
						(
							@do: () => Task.CompletedTask,
							undo
						);
			}
		
		extension(AsyncStep)
			{
				public static AsyncStep Backward
					(
						Action undo
					)
					=> Step.Backward(undo).AsAsync;
			}
	}