using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlFileInstancing
	{
		extension(IFile file)
			{
				public IFileIO<TomlTable> AsTomlIO
					=> new TomlFileIO(file);
			}
	}