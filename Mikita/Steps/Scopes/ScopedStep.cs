using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Scopes;

/// <summary>
/// Creates its inner step on <see cref="Do"/> and clears it on
/// <see cref="Undo"/>.
/// </summary>
public sealed class ScopedStep<T>
	(
		Func<T, IAsyncStep> pattern,
		IRef<IAsyncStep?> current,
		Func<T> value
	)
	: IAsyncStep
	{
		public Task Do()
			{
				current.SetTo(pattern(value()));
				return current.Value!.Do();
			}

		public async Task Undo()
			{
				await current.Value!.Undo();
				current.SetTo(null);
			}
	}