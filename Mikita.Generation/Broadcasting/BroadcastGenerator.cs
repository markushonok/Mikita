using Microsoft.CodeAnalysis;
using Mikita.Generation.Calling.Edges;
using Mikita.Generation.Calling.Graphs;
using Mikita.Generation.Calling.Roots;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Mikita.Generation.Broadcasting;

[Generator]
public sealed class BroadcastGenerator
	: IIncrementalGenerator
	{
		public void Initialize
			(
				IncrementalGeneratorInitializationContext context
			)
			{
				var roots = CallRoot.ProviderFrom(context).Collect();
				var edges = CallEdge.ProviderFrom(context).Collect();
				var calls = roots.Combine(edges);
				context.RegisterSourceOutput(calls, SourceOutput);
			}

		private static void SourceOutput
			(
				SourceProductionContext context,
				(ImmutableArray<ICallRoot> Roots, ImmutableArray<ICallEdge> Edges) calls
			)
			{
				var i = 1;
				foreach (var edge in calls.Edges)
					{
						if (!edge.Caller.ToDisplayString().Contains(".Events."))
							continue;

						if (edge.Caller.ToDisplayString().Contains("Subscription"))
							continue;

						context.ReportDiagnostic(
							Diagnostic.Create(
								new DiagnosticDescriptor(
									"BRD_EDGE",
									"Edge",
									$"#{i++} {edge.Caller.ToDisplayString()} -> {edge.Callee.ToDisplayString()}",
									"Broadcast",
									DiagnosticSeverity.Warning,
									isEnabledByDefault: true),
								Location.None));
					}

				// var testMethod = calls.Edges.First(
				// 	x => x.EnclosingMethod.ToDisplayString().Contains("TestMethod"));
				//
				// var ctor = calls.Edges.First(
				// 	x => x.EnclosingMethod.ToDisplayString().Contains("ReactionCollection.ReactionCollection"));
				//
				// context.ReportDiagnostic(
				// 	Diagnostic.Create(
				// 		new DiagnosticDescriptor(
				// 			"BRD_EDGE",
				// 			"Edge",
				// 			$"{testMethod.Callee.ConstructedFrom} == {ctor.EnclosingMethod.ConstructedFrom}: {SymbolEqualityComparer.Default.Equals(
				// 				testMethod.Callee.ConstructedFrom, ctor.EnclosingMethod.ConstructedFrom)}",
				// 			"Broadcast",
				// 			DiagnosticSeverity.Warning,
				// 			isEnabledByDefault: true),
				// 		Location.None));

				SourceOutput
					(
						context,
						calls.Roots,
						CallGraph.From(calls.Edges)
					);
			}

		private static void SourceOutput
			(
				SourceProductionContext context,
				IEnumerable<ICallRoot> roots,
				ICallGraph graph
			)
			=> SourceOutput
				(
					context,
					graph.SearchFor(roots, context).ToArray()
				);

		private static void SourceOutput
			(
				SourceProductionContext context,
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				foreach (var @interface in interfaces)
					AddBroadcastType(context, @interface);

				AddInitialization(context, interfaces);
			}

		private static void AddBroadcastType
			(
				SourceProductionContext context,
				INamedTypeSymbol @interface
			)
			{
				var template = Template.WithUniqueName("Broadcast.cs.txt");

				var fullName = @interface
					.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

				var source = template
					.Replace("{{NAME}}", @interface.Name)
					.Replace("{{INTERFACE}}", fullName)
					.Replace("{{BODY}}", BodyFor(@interface));

				context.AddSource($"{@interface.Name}Broadcast.g.cs", source);
			}

		private static string BodyFor
			(
				INamedTypeSymbol @interface
			)
			{
				var builder = new StringBuilder();

				var methods = @interface
					.GetMembers()
					.OfType<IMethodSymbol>()
					.Where(m => m.MethodKind == MethodKind.Ordinary);

				foreach (var method in methods)
					AppendMethod(builder, method);

				return builder.ToString();
			}

		private static void AppendMethod
			(
				StringBuilder builder,
				IMethodSymbol method
			)
			{
				var returnType = method
					.ReturnType
					.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

				var name = method.Name;
				var parameters = method.Parameters;

				builder.Append($"{Tab(2)}public ")
					.Append(returnType)
					.Append(' ')
					.Append(name)
					.Append('(');

				var first = true;
				foreach (var p in parameters)
					{
						if (!first) builder.Append(", ");
						first = false;

						var mod = p.RefKind switch
							{
								RefKind.Ref => "ref ",
								RefKind.Out => "out ",
								RefKind.In  => "in ",
								_           => string.Empty
							};

						builder.Append(mod)
							.Append(p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
							.Append(' ')
							.Append(p.Name);
					}

				builder.AppendLine(")");
				builder.AppendLine($"{Tab(3)}{{");

				var hasReturn = !method.ReturnsVoid;
				if (hasReturn)
					{
						builder.Append($"{Tab(4)}")
							.Append(returnType)
							.AppendLine(" result = default!;");
					}

				builder.AppendLine($"{Tab(4)}foreach (var listener in listeners)")
					.Append($"{Tab(5)}{{")
					.AppendLine();

				builder.Append($"{Tab(6)}");
				if (hasReturn)
					builder.Append("result = ");

				builder.Append("listener.")
					.Append(name)
					.Append('(');

				first = true;
				foreach (var parameter in parameters)
					{
						if (!first) builder.Append(", ");
						first = false;

						var mod = parameter.RefKind switch
							{
								RefKind.Ref => "ref ",
								RefKind.Out => "out ",
								RefKind.In => "in ",
								_ => string.Empty
							};

						builder.Append(mod).Append(parameter.Name);
					}

				builder.AppendLine(");");
				builder.AppendLine($"{Tab(5)}}}");

				if (hasReturn)
					{
						builder.AppendLine($"{Tab(4)}return result;");
					}

				builder.AppendLine($"{Tab(3)}}}");
				builder.AppendLine();
			}

		private void AddEmptyBroadcastFactory
			(
				IncrementalGeneratorPostInitializationContext context
			)
			{
				var text = Template.WithUniqueName("Initialization.cs.txt");
				context.AddSource("Broadcast.Empty.g.cs", text);
			}

		private static void AddInitialization
			(
				SourceProductionContext context,
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				var text = Template
					.WithUniqueName("Initialization.cs.txt")
					.Replace("{{BODY}}", InitializationBodyWith(interfaces));

				context.AddSource("Initialization.g.cs", text);
			}

		private static string InitializationBodyWith
			(
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				var builder = new StringBuilder();

				foreach (var @interface in interfaces)
					{
						builder.AppendLine(
							$"{Tab(3)}BroadcastRegistry.Add<{@interface}>(x => new {BroadcastName(@interface)}(x));");
					}

				return builder.ToString();
			}

		private static string BroadcastName
			(
				INamedTypeSymbol @interface
			)
			=> @interface.Name + "Broadcast";

		private static string Tab
			(
				int count = 1
			)
			=> new('\t', count);
	}