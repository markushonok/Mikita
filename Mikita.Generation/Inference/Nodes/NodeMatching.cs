using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Mikita.Generation.Inference.Nodes;

public static class NodeMatching
	{
		extension(INode node)
			{
				public bool Is(IOperation operation)
					{
						var symbol = SymbolOf(operation);

						if (symbol is null)
							return false;

						var otherName = symbol.ToDisplayString(Node.SymbolDisplayFormat);
						return node.Name == otherName;
					}
			}

		private static ISymbol? SymbolOf(IOperation operation)
			=> operation switch
				{
					IInvocationOperation x => x.TargetMethod,
					IMethodReferenceOperation x => x.Method,
					IObjectCreationOperation x => x.Constructor,
					IMemberReferenceOperation x => x.Member,
					_ => null
				};
	}