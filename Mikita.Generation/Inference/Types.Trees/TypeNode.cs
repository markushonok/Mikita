using System.Collections.Generic;

namespace Mikita.Generation.Inference.Types.Trees;

public sealed record TypeNode
	(
		string Name,
		IEnumerable<ITypeNode> Arguments
	)
	: ITypeNode;