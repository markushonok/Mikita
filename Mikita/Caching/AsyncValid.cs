using Mikita.Nulls;
using Mikita.Routines;
using Mikita.Structs.Referring;
using Mikita.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Caching;

public sealed class AsyncValid<T>
	(
		CancellableTask<T> fresh,
		IRef<Task<T>?> current,
		IRef<bool> isValid
	)
	: IAsyncValid<T>
	{
		public Task<T> Value
			(
				CancellationToken cancel = default
			)
			{
				cancel.ThrowIfRequested();
				if (!isValid.Value)
					{
						current.SetTo(fresh(cancel));
						isValid.TurnTrue();
					}
				return current.Value.NotNull();
			}

		public void Invalidate()
			=> isValid.TurnFalse();
	}