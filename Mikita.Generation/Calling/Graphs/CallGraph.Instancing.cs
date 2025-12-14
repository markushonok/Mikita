using Microsoft.CodeAnalysis;
using Mikita.Generation.Calling.Edges;
using System.Collections.Generic;

namespace Mikita.Generation.Calling.Graphs;

partial class CallGraph
	{
		public static CallGraph From
			(
				IEnumerable<ICallEdge> edges
			)
			{
				var graph = CallGraph.NewEmpty;

				foreach (var edge in edges)
					graph.Add(edge);

				return graph;
			}

		public static CallGraph NewEmpty
			=> new
				(
					new Dictionary
						<
							IMethodSymbol,
							ICollection<ICallEdge>
						>
						(
							SymbolEqualityComparer.Default
						)
				);
}