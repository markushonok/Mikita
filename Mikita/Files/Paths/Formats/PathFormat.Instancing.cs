namespace Mikita.Files.Paths.Formats;

public sealed partial class PathFormat
	{
		public static PathFormat Unix
			=> new("/");

		public static PathFormat Windows
			=> new("\\");
	}