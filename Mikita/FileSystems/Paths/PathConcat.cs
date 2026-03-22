using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Paths;

public static class PathConcat
	{
		extension(Path path)
			{
				public static IPath From
					(
						IEnumerable<IPath> paths
					)
					=> paths.Aggregate((left, right) => left / right);
			}

		extension(IPath path)
			{
				public static Path operator /
					(
						IPath left,
						IPath right
					)
					=> right.Ascends >= left.Elements.Count
						? right.BaseHigherBy(left.Ascends - left.Elements.Count)
						: left.TrimThenAppend(right);

				public static Path operator /
					(
						IPath left,
						string right
					)
					=> new
						(
							left.Elements.Append(right).ToArray(),
							left.Ascends
						);

				public Path TrimThenAppend
					(
						IPath right
					)
					{
						var merged = path.Elements
							.Take(path.Elements.Count - right.Ascends)
							.Concat(right.Elements)
							.ToArray();

						return new Path(merged, path.Ascends);
					}
			}
	}