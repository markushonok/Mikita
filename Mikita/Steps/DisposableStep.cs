using System;

namespace Mikita.Steps;

/// <summary>
/// Adapts a disposable resource to the step contract: nothing is done
/// on <see cref="IStep.Do"/>, and the resource is disposed on
/// <see cref="IStep.Undo"/>.
/// </summary>
public static class DisposableStep
	{
		extension(IDisposable disposable)
			{
				public IStep AsStep
					=> Step.That
						(
							@do: delegate {},
							undo: disposable.Dispose
						);

				public IAsyncStep AsAsyncStep
					=> disposable.AsStep.AsAsync;
			}
	}
