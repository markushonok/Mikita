using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlFileInstancing
	{
		extension(TomlFileIO)
			{
				public static IFileIO<TomlTable> On
					(
						IFile file
					)
					=> new TomlFileIO(file);
			}
	}