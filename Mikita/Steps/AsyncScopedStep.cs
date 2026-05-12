using Mikita.Nulls;
using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class AsyncScopedStep<T>
	(
		Func<T, IAsyncStep> pattern,
		IRef<IAsyncStep?> current,
		IRef<T?> value
	)
	: IAsyncStep
	{
		public Task Do()
			{
				current.SetTo(pattern(value.Value.NotNull()));
				return current.Value!.Do();
			}

		public async Task Undo()
			{
				await current.Value!.Undo();
				current.SetTo(null);
			}
	}