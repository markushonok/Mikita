using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.SymbolDisplayGlobalNamespaceStyle;
using static Microsoft.CodeAnalysis.SymbolDisplayMemberOptions;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace Mikita.Generation.Inference.Nodes;

public static class NodeSymbolDisplayFormat
	{
		extension(Node)
			{
				public static SymbolDisplayFormat SymbolDisplayFormat
					=> new
						(
							globalNamespaceStyle: Included,
							typeQualificationStyle: NameAndContainingTypesAndNamespaces,
							memberOptions: IncludeContainingType
						);
			}
	}