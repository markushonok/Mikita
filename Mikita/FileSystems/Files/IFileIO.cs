using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

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