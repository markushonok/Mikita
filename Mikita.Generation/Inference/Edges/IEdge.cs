using Mikita.Generation.Inference.Nodes;
using Mikita.Generation.Inference.Types.Trees;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Edges;

public interface IEdge
	{
		INode From { get; }

		INode To { get; }

		IEnumerable<ITypeNode> TypeArguments { get; }
	}