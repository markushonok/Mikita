using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SystemFile = System.IO.File;

namespace Mikita.FileSystems.Files;

public sealed class File
	(
		IPath path
	)
	: IFile
	{
		public Task<Stream> Open
			(
				CancellationToken cancel = default
			)
			=> Open
				(
					FileMode.Open,
					FileAccess.Read,
					FileShare.Read,
					cancel
				);

		public async Task<Stream> Open
			(
				FileMode mode,
				FileAccess access,
				FileShare share,
				CancellationToken cancel = default
			)
			=> await Task.Run
				(
					() => new FileStream
						(
							PathString,
							mode,
							access,
							share,
							bufferSize: 4096,
							options: FileOptions.Asynchronous
						),
					cancel
				);

		public Task Create
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => SystemFile.Create(PathString).DisposeAsync().AsTask(),
					cancel
				);

		public Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => SystemFile.Move(PathString, destination.ToDefaultString()),
					cancel
				);

		public Task Delete
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => SystemFile.Delete(PathString),
					cancel
				);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => SystemFile.Exists(PathString),
					cancel
				);

		public IPath Path
			=> path;

		private string PathString
			=> path.ToDefaultString();
	}