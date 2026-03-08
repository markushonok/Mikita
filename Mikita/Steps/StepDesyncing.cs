using Mikita.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public static class StepDesyncing
	{
		extension(IStep step)
			{
				public AsyncStep AsAsync
					=> Step.That
						(
							@do: cancel =>
								{
									cancel.ThrowIfRequested();
									step.Do();
									return Task.CompletedTask;
								},
							undo: cancel =>
								{
									cancel.ThrowIfRequested();
									step.Undo();
									return Task.CompletedTask;
								}
						);
			}
	}