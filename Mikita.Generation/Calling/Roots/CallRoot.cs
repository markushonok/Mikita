using Microsoft.CodeAnalysis;

namespace Mikita.Generation.Calling.Roots;

public sealed partial class CallRoot
	(
		IMethodSymbol caller,
		ITypeSymbol type,
		INamedTypeSymbol? instanceType
	)
	: ICallRoot
	{
		public IMethodSymbol EnclosingMethod
			=> caller;

		public ITypeSymbol TargetType
			=> type;

		public INamedTypeSymbol? EnclosingType
			=> instanceType;
	}