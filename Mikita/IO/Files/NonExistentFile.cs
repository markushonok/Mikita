using Mikita.IO.Paths;
using Mikita.IO.Paths.Formats;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public sealed partial class NonExistentFile
	{
		public static IReadOnlyFile At(IPath path)
			=> new NonExistentFile(path);
	}

public sealed partial class NonExistentFile
	(
		IPath path
	)
	: IReadOnlyFile
	{
		public Task<Stream> Open
			(
				CancellationToken cancel = default
			)
			=> Task.FromException<Stream>(NotFoundException);

		public Task<bool> Exists(CancellationToken cancel = default)
			=> Task.FromResult(false);

		public IPath Path
			=> path;

		private FileNotFoundException NotFoundException
			=> new
				(
					$"The file at '{path.ToDefaultString()}' does not exist.",
					path.ToDefaultString()
				);
	}