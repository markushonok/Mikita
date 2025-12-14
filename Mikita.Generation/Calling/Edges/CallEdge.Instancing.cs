using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mikita.Generation.Calling.Edges;

partial record CallEdge
	{
		public static IncrementalValuesProvider<ICallEdge> ProviderFrom
			(
				IncrementalGeneratorInitializationContext context
			)
			=> context.SyntaxProvider
				.CreateSyntaxProvider
					(
						static (node, _) => IsRelevantCallLikeNode(node),
						static (ctx, _) => CallEdge.From(ctx)
					)
				.Where(x => x is not null)
				.Select((x, _) => x!);

		// todo refactor
		private static bool IsRelevantCallLikeNode(SyntaxNode node)
			{
				if (node is InvocationExpressionSyntax) return true;

				if (node is ObjectCreationExpressionSyntax) return true;
				if (node is ImplicitObjectCreationExpressionSyntax) return true;
				if (node is ImplicitArrayCreationExpressionSyntax) return true;

				if (node is MemberAccessExpressionSyntax memberAccess)
					{
						if (memberAccess.Parent is InvocationExpressionSyntax inv &&
							ReferenceEquals(inv.Expression, memberAccess))
							return false;

						return true;
					}

				if (node is ConditionalAccessExpressionSyntax) return true;

				if (node is not SimpleNameSyntax simpleName) return false;
				switch (simpleName.Parent)
					{
						case MemberAccessExpressionSyntax mae when
							ReferenceEquals(mae.Name, simpleName):
						case MemberBindingExpressionSyntax mbe when
							ReferenceEquals(mbe.Name, simpleName):
							return false;
						default:
							return false;
					}
			}

		public static ICallEdge? From
			(
				GeneratorSyntaxContext context
			)
			=> CallEdge.From
				(
					(ExpressionSyntax) context.Node,
					context.SemanticModel
				);

		public static ICallEdge? From
			(
				ExpressionSyntax expression,
				SemanticModel model
			)
			{
				var symbol = model.GetSymbolInfo(expression).Symbol;

				var callee = MethodBehind(symbol);

				if (callee is null)
					return null;

				var enclosing = model.GetEnclosingSymbol(expression.SpanStart);

				var caller = MethodBehind(enclosing);

				return caller is not null
					? new CallEdge(caller, callee)
					: null;
			}

		private static IMethodSymbol? MethodBehind
			(
				ISymbol? symbol
			)
			=> symbol switch
				{
					IMethodSymbol method => method,
					IPropertySymbol property => property.GetMethod,
					IFieldSymbol field => MethodBehind(field.AssociatedSymbol),
					_ => null
				};
	}