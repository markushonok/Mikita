using System.Collections.Generic;

namespace Mikita.Files.Paths;

public sealed class Path
	(
		IEnumerable<string> elements
	)
	: IPath
	{
		public override string ToString()
			=> string.Join('/', elements);

		public IEnumerable<string> Elements
			=> elements;
	}