using Microsoft.CodeAnalysis;
using Mikita.Generation.Calling.Roots;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Generation.Calling.Graphs;

public static class GraphSearch
	{
		public static IEnumerable<INamedTypeSymbol> SearchFor
			(
				this ICallGraph graph,
				IEnumerable<ICallRoot> roots,
				SourceProductionContext context
			)
			{
				var queue = new Queue<ICallRoot>();
				var visited = new HashSet<ICallRoot>();

				foreach (var root in roots)
					{
						queue.Enqueue(root);
						visited.Add(root);
					}

				while (queue.Count > 0)
					{
						var current = queue.Dequeue();

						if
							(
								current.TargetType is INamedTypeSymbol named &&
								named.TypeKind == TypeKind.Interface
							)
							{
								yield return named;
								continue;
							}

						context.ReportDiagnostic(
							Diagnostic.Create(
								new DiagnosticDescriptor(
									"BRD_EDGE",
									"Edge",
									$"{current.EnclosingMethod.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}: {graph.IncomingTo(current.EnclosingMethod).Count()}",
									"Broadcast",
									DiagnosticSeverity.Warning,
									isEnabledByDefault: true),
								Location.None));

						foreach (var edge in graph.IncomingTo(current.EnclosingMethod))
							{
								var nextType = NextTypeFor(edge.Callee, current.TargetType);
								var step = CallRoot.With(edge.Caller, nextType);

								if (!visited.Add(step))
									continue;

								queue.Enqueue(step);
							}
					}
			}

		private static ITypeSymbol NextTypeFor
			(
				IMethodSymbol callee,
				ITypeSymbol type
			)
			{
				if (type is not ITypeParameterSymbol typeParameter) return type;

				switch (typeParameter.TypeParameterKind)
					{
						case TypeParameterKind.Method:
							{
								var index = callee.TypeParameters.IndexOf(typeParameter);
								if (index < 0) return type;

								return callee.TypeArguments.Length == callee.TypeParameters.Length
									? callee.TypeArguments[index]
									: type;
							}

						case TypeParameterKind.Type:
							{
								var containing = callee.ContainingType;
								if (containing is null)
									return type;

								var index = containing.TypeParameters.IndexOf(typeParameter);
								if (index < 0) return type;

								// Для generic-типа ReactionCollection<T> при конкретизации ReactionCollection<IStep>
								// TypeArguments[index] как раз будет IStep
								return containing.TypeArguments.Length == containing.TypeParameters.Length
									? containing.TypeArguments[index]
									: type;
							}

						default:
							return type;
					}
			}
	}