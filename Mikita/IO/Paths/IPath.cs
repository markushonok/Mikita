using System.Collections.Generic;

namespace Mikita.IO.Paths;

public interface IPath
	{
		IReadOnlyList<string> Elements { get; }

		int Ascends { get; }
	}