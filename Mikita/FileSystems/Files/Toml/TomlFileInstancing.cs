namespace Mikita.FileSystems.Files.Toml;

public static class TomlFileInstancing
	{
		extension(IFile file)
			{
				public IFileIO<ITomlTable> AsTomlIO
					=> new TomlFileIO(file);
			}
	}