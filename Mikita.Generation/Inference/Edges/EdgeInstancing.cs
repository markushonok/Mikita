using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Mikita.Generation.Inference.Nodes;
using Mikita.Generation.Inference.Types.Trees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Generation.Inference.Edges;

public static class EdgeInstancing
	{
		extension(Edge)
			{
				public static Edge? OrNullFrom
					(
						IOperation operation
					)
					{
						var fromSymbol = EnclosingSymbolOf(operation);
						if (fromSymbol is null) return null;

						var toSymbol = SymbolOf(operation);
						if (toSymbol is null) return null;

						var fromNode = Node.From(fromSymbol);
						var toNode = Node.From(toSymbol);

						var typeArguments = TypeArguments
							(
								fromNode,
								fromSymbol,
								toSymbol
							);

						return new Edge(fromNode, toNode, typeArguments);
					}
			}

		private static ISymbol? EnclosingSymbolOf
			(
				IOperation operation
			)
			=> operation
				.SemanticModel
				?.GetEnclosingSymbol(operation.Syntax.SpanStart);

		private static ISymbol? SymbolOf
			(
				IOperation operation
			)
			=> operation switch
				{
					IInvocationOperation x => x.TargetMethod,
					IMethodReferenceOperation x => x.Method,
					IObjectCreationOperation x => x.Constructor,
					IMemberReferenceOperation x => x.Member,
					_ => null
				};

		private static IEnumerable<ITypeNode> TypeArguments
			(
				INode fromNode,
				ISymbol fromSymbol,
				ISymbol toSymbol
			)
			{
				var fromTypeParams = TypeParameterMap(fromNode, fromSymbol);
				var actualTypeArgs = ActualTypeArguments(toSymbol);

				if (actualTypeArgs.Count == 0) return [];

				return actualTypeArgs
					.Select(arg => ToTypeNode(arg, fromTypeParams))
					.ToArray();
			}

		private static IReadOnlyList<ITypeSymbol> ActualTypeArguments
			(
				ISymbol toSymbol
			)
			=> toSymbol switch
				{
					// Конструктор generic-типа: берем аргументы типа
					IMethodSymbol m when m.MethodKind == MethodKind.Constructor
						=> m.ContainingType is INamedTypeSymbol ct
							? ct.TypeArguments
							: Array.Empty<ITypeSymbol>(),

					IMethodSymbol x => x.TypeArguments,
					INamedTypeSymbol x => x.TypeArguments,
					_ => []
				};

		private static IReadOnlyDictionary<ITypeParameterSymbol, string> TypeParameterMap
			(
				INode fromNode,
				ISymbol fromSymbol
			)
			{
				if (fromSymbol is not INamedTypeSymbol type)
					type = fromSymbol.ContainingType;

				if (type is null)
					return new Dictionary<ITypeParameterSymbol, string>(SymbolEqualityComparer.Default);

				var result = new Dictionary<ITypeParameterSymbol, string>(SymbolEqualityComparer.Default);
				var names = fromNode.TypeParameters.ToArray();

				for (int i = 0; i < type.TypeParameters.Length && i < names.Length; i++)
					result[type.TypeParameters[i]] = names[i];

				return result;
			}

		private static ITypeNode ToTypeNode
			(
				ITypeSymbol typeSymbol,
				IReadOnlyDictionary<ITypeParameterSymbol, string> typeParamMap
			)
			{
				// Параметр типа *контейнера* → маппим в параметр узла
				if (typeSymbol is ITypeParameterSymbol tp &&
					typeParamMap.TryGetValue(tp, out var name))
					return new TypeNode(name, Array.Empty<ITypeNode>());

				// Параметр метода: просто имя U (оно не фигурирует в Node.TypeParameters)
				if (typeSymbol is ITypeParameterSymbol methodTp)
					return new TypeNode(methodTp.Name, Array.Empty<ITypeNode>());

				if (typeSymbol is INamedTypeSymbol named)
					{
						var fullName = named.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

						if (named.TypeArguments.Length == 0)
							return new TypeNode(fullName, Array.Empty<ITypeNode>());

						var args = new ITypeNode[named.TypeArguments.Length];
						for (int i = 0; i < named.TypeArguments.Length; i++)
							args[i] = ToTypeNode(named.TypeArguments[i], typeParamMap);

						return new TypeNode(fullName, args);
					}

				if (typeSymbol is IArrayTypeSymbol array)
					{
						var element = ToTypeNode(array.ElementType, typeParamMap);
						var arrayName = array.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
						return new TypeNode(arrayName, new[] { element });
					}

				var fqn = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
				return new TypeNode(fqn, Array.Empty<ITypeNode>());
			}
	}