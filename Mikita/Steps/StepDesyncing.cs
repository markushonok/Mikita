using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Adapts synchronous steps to the asynchronous step contract.
/// </summary>
public static class StepDesyncing
	{
		extension(IStep step)
			{
				public IAsyncStep AsAsync
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