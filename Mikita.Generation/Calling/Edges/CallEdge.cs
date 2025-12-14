using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mikita.Generation.Calling.Edges;

public sealed partial record CallEdge
	(
		IMethodSymbol Caller,
		IMethodSymbol Callee
	)
	: ICallEdge;