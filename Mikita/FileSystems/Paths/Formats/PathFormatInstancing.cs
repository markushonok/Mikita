using System;
using System.Collections.Generic;

namespace Mikita.FileSystems.Paths.Formats;

public static class PathFormatInstancing
	{
		extension(PathFormat)
			{
				/// <summary>
				/// Uses `/` as a path separator (Unix-style).
				/// </summary>
				public static PathFormat Unix
					=> new("/");

				/// <summary>
				/// Uses `\` as a path separator (Windows-style).
				/// </summary>
				public static PathFormat Windows
					=> new(@"\");

				/// <summary>
				/// Uses `.` as a path separator.
				/// </summary>
				public static PathFormat DotSeparated
					=> new(".");

				public static IReadOnlySet<PathFormat> All
					=> new HashSet<PathFormat>
						{
							PathFormat.Unix,
							PathFormat.Windows,
							PathFormat.DotSeparated
						};

				public static InvalidOperationException UnknownPathFormatException
					(
						string formattedPath
					)
					=> new($"""Unable to determine path format of "{formattedPath}".""");
			}
	}