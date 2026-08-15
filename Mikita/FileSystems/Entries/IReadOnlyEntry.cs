using Mikita.FileSystems.Paths;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Entries;

public interface IReadOnlyEntry
	{
		Task<bool> Exists
			(
				CancellationToken cancel = default
			);

		IPath Path { get; }
	}