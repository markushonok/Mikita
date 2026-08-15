using Mikita.FileSystems.Entries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public interface IReadOnlyFile: IReadOnlyEntry
	{
		Task<Stream> Open
			(
				CancellationToken cancel = default
			);
	}