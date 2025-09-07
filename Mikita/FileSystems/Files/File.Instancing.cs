using Mikita.FileSystems.Paths;

namespace Mikita.FileSystems.Files;

partial class File
	{
		public static File At(IPath path)
			=> new(path);
	}