using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepDesyncing
	{
		extension(IStep step)
			{
				public AsyncStep AsAsync
					=> Step.That
						(
							@do: () =>
								{
									step.Do();
									return Task.CompletedTask;
								},
							undo: () =>
								{
									step.Undo();
									return Task.CompletedTask;
								}
						);
			}
	}