using Mikita.FileSystems.Entries;
using Mikita.Routines;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public sealed class EnsuredFileIO<T>
	(
		IFile file,
		IFileIO<T> fileIO,
		CancellableTask<T> @default
	)
	: IFileIO<T>
	{
		public async Task<T> Load
			(
				CancellationToken cancel = default
			)
			=> await file.Exists(cancel)
				? await fileIO.Load(cancel)
				: await @default(cancel);

		public async Task Save
			(
				T target,
				CancellationToken cancel = default
			)
			{
				if (await file.NotExists(cancel))
					await file.Create(cancel);

				await fileIO.Save(target, cancel);
			}
	}