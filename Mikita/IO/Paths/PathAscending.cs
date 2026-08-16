using System.Collections.Generic;
using System.Linq;

namespace Mikita.IO.Paths;

public static class PathAscending
	{
		extension(IPath path)
			{
				public IPath Parent
					=> path.HigherBy(1);

				public IEnumerable<IPath> Ancestors
					=> Enumerable
						.Range(1, path.Elements.Count)
						.Select(path.HigherBy);

				public IPath HigherBy(int steps)
					=> new Path
						(
							path.Elements.ToArray()[..^steps],
							path.Ascends
						);

				public IPath BaseHigherBy(int steps)
					=> new Path
						(
							path.Elements.ToArray(),
							path.Ascends + steps
						);
			}
	}