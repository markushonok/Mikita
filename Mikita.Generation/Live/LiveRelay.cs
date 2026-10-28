using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Mikita.Generation.Live;

public sealed class LiveRelay
	(
		INamedTypeSymbol type,
		ImmutableArray<ISymbol> members
	)
	: ILiveRelay
	{
		public INamedTypeSymbol Type
			=> type;

		public string Declaration
			{
				get
					{
						var text = new StringBuilder(Header);

						foreach (var member in members)
							text.Append(MemberText(member));

						return text
							.Append(ObjectOverrides)
							.Append("\t}\n")
							.ToString();
					}
			}

		public string Registration
			=> "\t\t\t\tglobal::Mikita.Evaluation.Liveness.LiveRelay<"
				+ TypeName
				+ ">.Factory = static function => new "
				+ Name
				+ "(function);\n";

		private string Header
			=> "\ninternal sealed class "
				+ Name
				+ "\n\t: "
				+ TypeName
				+ "\n\t{\n"
				+ FieldText
				+ ConstructorText;

		private string FieldText
			=> "\t\tprivate readonly global::System.Func<"
				+ TypeName
				+ "> function;\n\n";

		private string ConstructorText
			=> "\t\tinternal "
				+ Name
				+ "(global::System.Func<"
				+ TypeName
				+ "> function)\n"
				+ "\t\t\t=> this.function = function;\n";

		public string Name
			{
				get
					{
						var display = TypeName.Replace("global::", "");
						var name = new StringBuilder("Live_");

						foreach (var symbol in display)
							name.Append
								(
									char.IsLetterOrDigit(symbol)
										? symbol
										: '_'
								);

						return name.ToString();
					}
			}

		private string TypeName
			=> LiveSyntax.Reference(type);

		private static string MemberText(ISymbol member)
			=> member switch
				{
					IPropertySymbol property
						when property.IsIndexer => IndexerText(property),
					IPropertySymbol property => PropertyText(property),
					IMethodSymbol method => MethodText(method),
					IEventSymbol @event => EventText(@event),
					_ => ""
				};

		private static string PropertyText(IPropertySymbol property)
			=> PropertyHeader(property)
				+ PropertyAccessors(property)
				+ "\t\t\t}\n";

		private static string PropertyHeader(IPropertySymbol property)
			=> "\n\t\t"
				+ LiveSyntax.Reference(property.Type)
				+ " "
				+ LiveSyntax.Explicit(property)
				+ "\n\t\t\t{\n";

		private static string PropertyAccessors
			(
				IPropertySymbol property
			)
			{
				var source = Access(property);
				var name = LiveSyntax.Identifier(property.Name);
				var text = "";

				if (property.GetMethod is not null)
					text += "\t\t\t\tget => "
						+ source
						+ "."
						+ name
						+ ";\n";

				if (property.SetMethod is not null)
					text += "\t\t\t\tset => "
						+ source
						+ "."
						+ name
						+ " = value;\n";

				return text;
			}

		private static string IndexerText(IPropertySymbol property)
			{
				var text = new StringBuilder("\n\t\t");

				text
					.Append(LiveSyntax.Reference(property.Type))
					.Append(' ')
					.Append(LiveSyntax.Reference(property.ContainingType))
					.Append(".this[")
					.Append(IndexerParameters(property))
					.Append("]\n\t\t\t{\n")
					.Append(IndexerAccessors(property));

				return text.Append("\t\t\t}\n").ToString();
			}

		private static string IndexerParameters
			(
				IPropertySymbol property
			)
			=> string.Join
				(
					", ",
					property.Parameters.Select
						(
							x => LiveSyntax.Reference(x.Type)
								+ " "
								+ IndexerName(x)
						)
				);

		private static string IndexerAccessors
			(
				IPropertySymbol property
			)
			{
				var source = Access(property);
				var arguments = string.Join
					(
						", ",
						property.Parameters.Select(IndexerName)
					);
				var text = "";

				if (property.GetMethod is not null)
					text += "\t\t\t\tget => "
						+ source
						+ "["
						+ arguments
						+ "];\n";

				if (property.SetMethod is not null)
					text += "\t\t\t\tset => "
						+ source
						+ "["
						+ arguments
						+ "] = value;\n";

				return text;
			}

		private static string IndexerName(IParameterSymbol parameter)
			=> LiveSyntax.Identifier
				(
					parameter.Name == "value"
						? "item"
						: parameter.Name
				);

		private static string MethodText(IMethodSymbol method)
			=> "\n\t\t"
				+ LiveSyntax.Reference(method.ReturnType)
				+ " "
				+ LiveSyntax.Explicit(method)
				+ LiveSyntax.TypeParameters(method)
				+ "("
				+ LiveSyntax.Parameters(method)
				+ ")"
				+ LiveSyntax.Constraints(method)
				+ "\n\t\t\t=> "
				+ Access(method)
				+ "."
				+ LiveSyntax.Identifier(method.Name)
				+ LiveSyntax.TypeArguments(method)
				+ "("
				+ LiveSyntax.Arguments(method)
				+ ");\n";

		private static string EventText(IEventSymbol @event)
			{
				var source = Access(@event);
				var name = LiveSyntax.Identifier(@event.Name);

				return "\n\t\tevent "
					+ LiveSyntax.Reference(@event.Type)
					+ " "
					+ LiveSyntax.Explicit(@event)
					+ "\n\t\t\t{\n"
					+ "\t\t\t\tadd => "
					+ source
					+ "."
					+ name
					+ " += value;\n"
					+ "\t\t\t\tremove => "
					+ source
					+ "."
					+ name
					+ " -= value;\n"
					+ "\t\t\t}\n";
			}

		private static string Access(ISymbol member)
			=> "(("
				+ LiveSyntax.Reference(member.ContainingType)
				+ ") function())";

		private const string ObjectOverrides
			= "\n\t\tpublic override global::System.String? ToString()\n"
				+ "\t\t\t=> function().ToString();\n"
				+ "\n\t\tpublic override global::System.Int32 GetHashCode()\n"
				+ "\t\t\t=> function().GetHashCode();\n"
				+ "\n\t\tpublic override global::System.Boolean Equals"
				+ "(global::System.Object? value)\n"
				+ "\t\t\t=> function().Equals(value);\n";
	}