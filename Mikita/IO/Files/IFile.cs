using Mikita.IO.Entries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public interface IFile: IReadOnlyFile, IEntry
	{
		Task<Stream> Open
			(
				FileMode mode,
				FileAccess access,
				FileShare share,
				CancellationToken cancel = default
			);
	}