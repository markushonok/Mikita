using Mikita.IO.Entries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public interface IReadOnlyFile: IReadOnlyEntry
	{
		Task<Stream> Open
			(
				CancellationToken cancel = default
			);
	}