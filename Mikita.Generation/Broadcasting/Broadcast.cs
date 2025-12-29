using Microsoft.CodeAnalysis;
using Mikita.Generation.Targets;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Mikita.Generation.Broadcasting;

public static class Broadcast
	{
		public static void AddSourceFiles
			(
				SourceProductionContext context,
				ImmutableArray<INamedTypeSymbol> targets
			)
			{
				var interfaces = targets
					.OfType<INamedTypeSymbol>()
					.Where(t => t.TypeKind == TypeKind.Interface)
					.Distinct(SymbolEqualityComparer.Default)
					.Cast<INamedTypeSymbol>();

				Broadcast.AddSourceFiles(context, interfaces);
			}

		public static void AddSourceFiles
			(
				SourceProductionContext context,
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				foreach (var @interface in interfaces)
					AddBroadcastType(context, @interface);

				BroadcastInit.AddSourceFile(context, interfaces);
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

				builder.Append($"{Formatting.Tab(2)}public ")
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
				builder.AppendLine($"{Formatting.Tab(3)}{{");

				var hasReturn = !method.ReturnsVoid;
				if (hasReturn)
					{
						builder.Append($"{Formatting.Tab(4)}")
							.Append(returnType)
							.AppendLine(" result = default!;");
					}

				builder.AppendLine($"{Formatting.Tab(4)}foreach (var listener in listeners)")
					.Append($"{Formatting.Tab(5)}{{")
					.AppendLine();

				builder.Append($"{Formatting.Tab(6)}");
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
				builder.AppendLine($"{Formatting.Tab(5)}}}");

				if (hasReturn)
					{
						builder.AppendLine($"{Formatting.Tab(4)}return result;");
					}

				builder.AppendLine($"{Formatting.Tab(3)}}}");
				builder.AppendLine();
			}
	}