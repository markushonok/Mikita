using Mikita.FileSystems.Entries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public interface IFile: ILocatedEntry
	{
		Task<Stream> Open
			(
				FileMode mode = FileMode.Open,
				FileAccess access = FileAccess.ReadWrite,
				FileShare share = FileShare.Read,
				CancellationToken cancel = default
			);
	}