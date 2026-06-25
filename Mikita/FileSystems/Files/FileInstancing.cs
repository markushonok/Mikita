using Mikita.FileSystems.Paths;
using System;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public static class FileInstancing
	{
		extension(File)
			{
				public static File At(IPath path)
					=> new(path);
			}

		extension<T>(IFileIO<T> fileIO)
			{
				public IFileIO<T> WithDefault
					(
						IFile file,
						Func<T> value
					)
					=> new EnsuredFileIO<T>
						(
							file,
							fileIO,
							cancel => Task.FromResult(value())
						);
			}
	}