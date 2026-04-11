using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Structs.Maps;

public interface IAsyncMap<in TKey, T>
	{
		Task<T> ValueWith
			(
				TKey key,
				CancellationToken cancel = default
			);
	}