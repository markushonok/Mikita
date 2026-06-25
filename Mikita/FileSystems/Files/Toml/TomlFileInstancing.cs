using System;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlFileInstancing
	{
		extension(IFile file)
			{
				public IFileIO<ITomlTable> AsSafeTomlIO
					=> file.AsTomlIO().WithDefault
						(
							file,
							value: () => new TomlTable().AsAbstract
						);

				public IFileIO<ITomlTable> AsTomlIO
					(
						Func<ITomlTable> @default
					)
					=> new TomlFileIO(file).WithDefault(file, @default);

				public IFileIO<ITomlTable> AsTomlIO()
					=> new TomlFileIO(file);
			}
	}