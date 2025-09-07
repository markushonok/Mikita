using System.Collections.Generic;

namespace Mikita.FileSystems.Paths;

public partial interface IPath
	{
		IReadOnlyCollection<string> Elements { get; }

		int Ascends { get; }
	}