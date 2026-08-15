using Mikita.IO.Paths;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Entries;

public interface IEntry: IReadOnlyEntry
	{
		Task Create
			(
				CancellationToken cancel = default
			);

		Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			);

		Task Delete
			(
				CancellationToken cancel = default
			);
	}