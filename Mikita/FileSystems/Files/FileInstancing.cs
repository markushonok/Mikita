using Mikita.FileSystems.Paths;

namespace Mikita.FileSystems.Files;

public static class FileInstancing
	{
		extension(File)
			{
				public static File At(IPath path)
					=> new(path);
			}
	}