using Mikita.Generation.Inference.Nodes;
using Mikita.Generation.Inference.Types.Trees;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Edges;

public sealed record Edge
	(
		INode From,
		INode To,
		IEnumerable<ITypeNode> TypeArguments
	)
	: IEdge;