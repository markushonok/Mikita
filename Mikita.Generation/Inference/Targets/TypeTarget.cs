using Mikita.Generation.Inference.Types.Trees;
using Mikita.Generation.Targets;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Targets;

public sealed record TypeTarget
	(
		IEnumerable<string> TypeParameters, 
		IEnumerable<ITypeNode> Transformations, 
		TargetProducts Products
	)
	: ITypeTarget;