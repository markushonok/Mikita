using Microsoft.CodeAnalysis;
using Mikita.Generation.Calling.Edges;
using System.Collections.Generic;

namespace Mikita.Generation.Calling.Graphs;

public sealed partial class CallGraph
	(
		IDictionary<IMethodSymbol, ICollection<ICallEdge>> dictionary
	)
	: ICallGraph
	{
		public IEnumerable<ICallEdge> IncomingTo
			(
				IMethodSymbol callee
			)
			=> dictionary.TryGetValue(callee.ConstructedFrom, out var edges)
				? edges
				: [];

		public void Add(ICallEdge edge)
			{
				var key = edge.Callee.ConstructedFrom;
				if (!dictionary.TryGetValue(key, out var callers))
					{
						callers = new HashSet<ICallEdge>();
						dictionary[key] = callers;
					}

				callers.Add(edge);
			}

		public int Count
			=> dictionary.Count;
	}