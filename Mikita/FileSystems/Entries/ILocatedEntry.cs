using Mikita.FileSystems.Paths;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Entries;

public interface ILocatedEntry
	{
		Task Create
			(
				CancellationToken cancel = default
			);

		Task Delete
			(
				CancellationToken cancel = default
			);

		Task<bool> Exists
			(
				CancellationToken cancel = default
			);

		IPath Path { get; }
	}