using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public interface IFileIO<T>
	{
		Task<T> Load
			(
				CancellationToken cancel = default
			);

		Task Save
			(
				T target,
				CancellationToken cancel = default
			);
	}