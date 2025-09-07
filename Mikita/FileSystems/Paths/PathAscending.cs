using System.Linq;

namespace Mikita.FileSystems.Paths;

public static class PathAscending
	{
		public static Path BaseAscendedBy
			(
				this IPath path,
				int steps
			)
			=> new
				(
					path.Elements.ToArray(),
					path.Ascends + steps
				);
	}