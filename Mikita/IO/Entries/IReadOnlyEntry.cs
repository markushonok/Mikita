using Mikita.IO.Paths;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Entries;

public interface IReadOnlyEntry
	{
		Task<bool> Exists
			(
				CancellationToken cancel = default
			);

		IPath Path { get; }
	}