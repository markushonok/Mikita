using Mikita.Generation.Inference.Edges;
using Mikita.Generation.Inference.Nodes;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Graphs;

public sealed class Graph
	(
		IDictionary<INode, IList<IEdge>> incoming,
		IDictionary<INode, IList<IEdge>> outgoing
	)
	: IGraph
	{
		public IEnumerable<IEdge> IncomingTo
			(
				INode node
			)
			=> incoming.TryGetValue(node, out var edges)
				? edges
				: [];

		public IEnumerable<IEdge> OutgoingFrom
			(
				INode node
			)
			=> outgoing.TryGetValue(node, out var edges)
				? edges
				: [];

		public void Add(IEdge edge)
			{
				if (!incoming.TryGetValue(edge.To, out var inList))
					{
						inList = [];
						incoming.Add(edge.To, inList);
					}

				if (!outgoing.TryGetValue(edge.From, out var outList))
					{
						outList = [];
						outgoing.Add(edge.From, outList);
					}

				inList.Add(edge);
				outList.Add(edge);
			}
	}