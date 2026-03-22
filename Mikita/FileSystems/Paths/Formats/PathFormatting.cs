namespace Mikita.FileSystems.Paths.Formats;

public static class PathFormatting
	{
		extension(IPath path)
			{
				public string With
					(
						IPathFormat format
					)
					{
						format.ApplyTo(path, out var formatted);
						return formatted;
					}

				public string ToDefaultString()
					=> path.With(PathFormat.Unix);
			}
	}