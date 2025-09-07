namespace Mikita.FileSystems.Paths.Formats;

public static class PathFormatting
	{
		public static string With
			(
				this IPath path,
				IPathFormat format
			)
			{
				format.ApplyTo(path, out var formatted);
				return formatted;
			}

		public static string ToDefaultString(this IPath path)
			=> path.With(PathFormat.Unix);
	}