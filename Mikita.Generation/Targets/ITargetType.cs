using System.Collections.Generic;

namespace Mikita.Generation.Targets;

public interface ITargetType
	{
		string Name { get; }

		int Arity { get; }

		TargetProducts Products { get; }
	}