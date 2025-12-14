using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Mikita.Generation.Calling.Roots;

partial class CallRoot
	{
		public static IncrementalValuesProvider<ICallRoot> ProviderFrom
			(
				IncrementalGeneratorInitializationContext context
			)
			=> context.SyntaxProvider
				.CreateSyntaxProvider(
					static (node, _) => node is InvocationExpressionSyntax,
					static (ctx, _) => CallRoot.From(ctx))
				.Where(x => x is not null)
				.Select((x, _) => x!);

		public static ICallRoot? From
			(
				GeneratorSyntaxContext context
			)
			{
				var invocation = (InvocationExpressionSyntax) context.Node;
				var symbol = context.SemanticModel.GetSymbolInfo(invocation).Symbol;

				if
					(
						symbol is not IMethodSymbol
							{
								Name: "To",
								ContainingType:
									{
										Name: "Broadcast",
										ContainingNamespace.Name: "Observation"
									},
								TypeArguments.Length: 1
							} method
					)
					{
						return null;
					}

				// метод, в котором находится вызов Broadcast.To
				var enclosing = context
					.SemanticModel
					.GetEnclosingSymbol(invocation.SpanStart);

				var caller = enclosing switch
					{
						IMethodSymbol m => m,                                   // обычный метод / ctor
						IPropertySymbol p => p.ContainingType.InstanceConstructors.FirstOrDefault(), // init свойства
						IFieldSymbol f    => f.ContainingType.InstanceConstructors.FirstOrDefault(), // init поля
						ITypeSymbol t     => t.ContainingType.InstanceConstructors.FirstOrDefault(),                // init на уровне типа
						_ => null
					};

				return caller is not null
					? CallRoot.With(caller, method.TypeArguments[0])
					: null;
			}

		public static CallRoot With
			(
				IMethodSymbol caller,
				ITypeSymbol type
			)
			=> new(caller, type);
	}