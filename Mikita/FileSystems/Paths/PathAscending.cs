using System.Linq;

namespace Mikita.FileSystems.Paths;

public static class PathAscending
	{
		extension(IPath path)
			{
				public Path HigherBy(int steps)
					=> new
						(
							path.Elements.ToArray()[..^steps],
							path.Ascends
						);

				public Path BaseHigherBy(int steps)
					=> new
						(
							path.Elements.ToArray(),
							path.Ascends + steps
						);
			}
	}