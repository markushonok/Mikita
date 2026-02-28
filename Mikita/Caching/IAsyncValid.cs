using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Caching;

public interface IAsyncValid<T>
	{
		Task<T> Value
			(
				CancellationToken cancel = default
			);

		void Invalidate();
	}