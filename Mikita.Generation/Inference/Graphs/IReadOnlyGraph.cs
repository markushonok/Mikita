using Mikita.Generation.Inference.Edges;
using Mikita.Generation.Inference.Nodes;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Graphs;

public interface IReadOnlyGraph
	{
		IEnumerable<IEdge> IncomingTo(INode node);

		IEnumerable<IEdge> OutgoingFrom(INode node);
	}