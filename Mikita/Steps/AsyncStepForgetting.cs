using Mikita.Threading;

namespace Mikita.Steps;

public static class AsyncStepForgetting
	{
		extension(IAsyncStep step)
			{
				public Step Forgot
					=> new
						(
							@do: () => step.Do().Forget(),
							undo: () => step.Undo().Forget()
						);
			}
	}