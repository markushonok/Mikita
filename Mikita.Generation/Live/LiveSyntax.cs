using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Generation.Live;

public static class LiveSyntax
	{
		public static string Explicit(ISymbol member)
			=> Reference(member.ContainingType)
				+ "."
				+ Identifier(member.Name);

		public static string Parameters(IMethodSymbol method)
			=> string.Join(", ", method.Parameters.Select(Parameter));

		public static string Arguments(IMethodSymbol method)
			=> string.Join(", ", method.Parameters.Select(Argument));

		public static string TypeParameters(IMethodSymbol method)
			=> TypeList(method);

		public static string TypeArguments(IMethodSymbol method)
			=> TypeList(method);

		public static string Constraints(IMethodSymbol method)
			=> string.Concat(method.TypeParameters.Select(Constraint));

		private static string TypeList(IMethodSymbol method)
			{
				if (method.TypeParameters.Length == 0)
					return "";

				var names = method
					.TypeParameters
					.Select(x => Identifier(x.Name));

				return "<" + string.Join(", ", names) + ">";
			}

		private static string Constraint(ITypeParameterSymbol parameter)
			{
				var clauses = ClausesOf(parameter);

				return clauses.Count == 0
					? ""
					: $" where {Identifier(parameter.Name)}: "
						+ string.Join(", ", clauses);
			}

		private static IReadOnlyList<string> ClausesOf
			(
				ITypeParameterSymbol parameter
			)
			{
				var clauses = new List<string>();
				var primary = PrimaryConstraint(parameter);

				if (primary is not null)
					clauses.Add(primary);

				clauses.AddRange(parameter.ConstraintTypes.Select(Reference));

				if (parameter.HasConstructorConstraint)
					clauses.Add("new()");

				return clauses;
			}

		private static string? PrimaryConstraint
			(
				ITypeParameterSymbol parameter
			)
			{
				if (parameter.HasUnmanagedTypeConstraint)
					return "unmanaged";

				if (parameter.HasValueTypeConstraint)
					return "struct";

				if (parameter.HasReferenceTypeConstraint)
					return ReferenceConstraint(parameter);

				return parameter.HasNotNullConstraint
					? "notnull"
					: null;
			}

		private static string ReferenceConstraint
			(
				ITypeParameterSymbol parameter
			)
			=> parameter.ReferenceTypeConstraintNullableAnnotation
			== NullableAnnotation.Annotated
				? "class?"
				: "class";

		private static string Parameter(IParameterSymbol parameter)
			=> Modifier(parameter.RefKind)
				+ Reference(parameter.Type)
				+ " "
				+ Identifier(parameter.Name);

		private static string Argument(IParameterSymbol parameter)
			=> parameter.RefKind switch
				{
					RefKind.Ref => "ref " + Identifier(parameter.Name),
					RefKind.Out => "out " + Identifier(parameter.Name),
					RefKind.In => "in " + Identifier(parameter.Name),
					RefKind.RefReadOnlyParameter
						=> "in " + Identifier(parameter.Name),
					_ => Identifier(parameter.Name)
				};

		private static string Modifier(RefKind kind)
			=> kind switch
				{
					RefKind.Ref => "ref ",
					RefKind.Out => "out ",
					RefKind.In => "in ",
					RefKind.RefReadOnlyParameter => "ref readonly ",
					_ => ""
				};

		public static string Reference(ITypeSymbol type)
			=> type.ToDisplayString(Format);

		public static string Identifier(string name)
			=> SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None
				? "@" + name
				: name;

		private static readonly SymbolDisplayFormat Format
			= SymbolDisplayFormat
				.FullyQualifiedFormat
				.AddMiscellaneousOptions
					(
						SymbolDisplayMiscellaneousOptions
							.IncludeNullableReferenceTypeModifier
					);
	}