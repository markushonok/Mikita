using Mikita.IO.Paths.Formats;
using System.Collections.Generic;

namespace Mikita.IO.Paths;

public sealed partial class Path
	(
		IReadOnlyList<string> elements,
		int ascends
	)
	: IPath
	{
		public override string ToString()
			=> this.ToDefaultString();

		public IReadOnlyList<string> Elements
			=> elements;

		public int Ascends
			=> ascends;
	}