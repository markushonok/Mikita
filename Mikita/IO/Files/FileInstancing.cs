using System;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public static class FileInstancing
	{
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