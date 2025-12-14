using Microsoft.CodeAnalysis;

namespace Mikita.Generation.Calling.Roots;

public interface ICallRoot
	{
		INamedTypeSymbol EnclosingType { get; }

		IMethodSymbol EnclosingMethod { get; }

		ITypeSymbol TargetType { get; }
	}