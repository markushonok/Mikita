using Microsoft.CodeAnalysis;
using Mikita.Generation.Inference.Edges;
using Mikita.Generation.Inference.Nodes;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Graphs;

public static class GraphInstancing
	{
		extension(Graph)
			{
				public static Graph With
					(
						Compilation compilation,
						IEnumerable<IOperation> operations
					)
					{
						var graph = Graph.NewEmpty;

						foreach (var operation in operations)
							{
								var edge = Edge.OrNullFrom(operation);
								if (edge is not null) graph.Add(edge);
							}

						return graph;
					}

				public static Graph NewEmpty
					=> new
						(
							new Dictionary<INode, IList<IEdge>>(),
							new Dictionary<INode, IList<IEdge>>()
						);
			}
	}