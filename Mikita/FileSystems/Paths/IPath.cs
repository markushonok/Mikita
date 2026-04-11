using System.Collections.Generic;

namespace Mikita.FileSystems.Paths;

public interface IPath
	{
		IReadOnlyList<string> Elements { get; }

		int Ascends { get; }
	}