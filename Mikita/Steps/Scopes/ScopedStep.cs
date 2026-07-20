using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Scopes;

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