using Microsoft.CodeAnalysis;
using Mikita.Generation.Calling.Edges;
using System.Collections.Generic;

namespace Mikita.Generation.Calling.Graphs;

public interface ICallGraph
	{
		IEnumerable<ICallEdge> IncomingTo
			(
				IMethodSymbol callee
			);

		void Add(ICallEdge edge);

		int Count { get; }
	}