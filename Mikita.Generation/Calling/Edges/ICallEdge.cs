using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mikita.Generation.Calling.Edges;

public interface ICallEdge
	{
		IMethodSymbol Caller { get; }

		IMethodSymbol Callee { get; }
	}