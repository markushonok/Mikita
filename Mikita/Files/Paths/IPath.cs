using System.Collections.Generic;

namespace Mikita.Files.Paths;

public partial interface IPath
	{
		IEnumerable<string> Elements { get; }
	}