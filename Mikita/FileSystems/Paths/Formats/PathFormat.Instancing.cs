using System;
using System.Collections.Generic;

namespace Mikita.FileSystems.Paths.Formats;

public sealed partial class PathFormat
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

		public static IReadOnlySet<PathFormat> All
			=> new HashSet<PathFormat>
				{
					PathFormat.Unix,
					PathFormat.Windows
				};

		public static InvalidOperationException UnknownPathFormatException
			(
				string formattedPath
			)
			=> new($"""Unable to determine path format of "{formattedPath}".""");
	}