namespace Mikita.Files.Paths.Formats;

public static class PathFormatting
	{
		public static string With
			(
				this IPath path,
				IPathFormat format
			)
			{
				format.Apply(path, out var formatted);
				return formatted;
			}
	}