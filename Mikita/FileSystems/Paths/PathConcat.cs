using System.Linq;

namespace Mikita.FileSystems.Paths;

public static class PathConcat
	{
		public static Path TrimThenAppend
			(
				this IPath left,
				IPath right
			)
			{
				var merged = left.Elements
					.Take(left.Elements.Count - right.Ascends)
					.Concat(right.Elements)
					.ToArray();

				return new Path(merged, left.Ascends);
			}
	}