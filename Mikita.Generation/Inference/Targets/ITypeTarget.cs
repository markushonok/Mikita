using Mikita.Generation.Inference.Types.Trees;
using Mikita.Generation.Targets;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Targets;

public interface ITypeTarget
	{
		IEnumerable<string> TypeParameters { get; }

		IEnumerable<ITypeNode> Transformations { get; }

		TargetProducts Products { get; }
	}