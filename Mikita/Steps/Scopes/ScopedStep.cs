using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Scopes;

/// <summary>
/// Creates its inner step on <see cref="Do"/> and clears it on
/// <see cref="Undo"/>.
/// </summary>
/// <remarks>
/// The pattern receives the initializer's result, including null when T
/// permits it. Calls must alternate as specified by <see cref="IAsyncStep"/>.
/// </remarks>
public sealed class ScopedStep<T>
	(
		Func<T, IAsyncStep> pattern,
		IRef<IAsyncStep?> current,
		Func<Task<T>> value
	)
	: IAsyncStep
	{
		public async Task Do()
			{
				current.SetTo(pattern(await value()));
				await current.Value!.Do();
			}

		public async Task Undo()
			{
				await current.Value!.Undo();
				current.SetTo(null);
			}
	}