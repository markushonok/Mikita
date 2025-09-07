using Mikita.FileSystems.Paths.Formats;
using System.Collections.Generic;

namespace Mikita.FileSystems.Paths;

public sealed partial class Path
	(
		IReadOnlyCollection<string> elements,
		int ascends
	)
	: IPath
	{
		public override string ToString()
			=> this.ToDefaultString();

		public IReadOnlyCollection<string> Elements
			=> elements;

		public int Ascends
			=> ascends;
	}