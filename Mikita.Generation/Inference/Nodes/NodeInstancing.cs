using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Generation.Inference.Nodes;

public static class NodeInstancing
	{
		extension(Node)
			{
				public static Node From
					(
						ISymbol symbol
					)
					=> new
						(
							Node.NameFrom(symbol),
							symbol.TypeParameterNames
						);

				public static string NameFrom
					(
						ISymbol symbol
					)
					=> symbol.ToDisplayString(Node.SymbolDisplayFormat);
			}

		extension(ISymbol symbol)
			{
				private IEnumerable<string> TypeParameterNames
					=> symbol.TypeParameters.Select(x => x.Name);

				private IReadOnlyList<ITypeParameterSymbol> TypeParameters
					=> symbol switch
						{
							INamedTypeSymbol x => x.TypeParameters,
							IMethodSymbol x => x.TypeParameters,
							_ => Array.Empty<ITypeParameterSymbol>()
						};
			}
	}