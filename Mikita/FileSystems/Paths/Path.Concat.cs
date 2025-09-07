using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Paths;

partial class Path
	{
		public static IPath From
			(
				IEnumerable<IPath> paths
			)
			=> paths.Aggregate((left, right) => left / right);

		public static Path operator /
			(
				Path left,
				string right
			)
			=> (IPath) left / right;
	}