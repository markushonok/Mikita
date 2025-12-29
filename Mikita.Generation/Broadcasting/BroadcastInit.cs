using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Mikita.Generation.Broadcasting;

public static class BroadcastInit
	{
		public static void AddSourceFile
			(
				SourceProductionContext context,
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				var text = Template
					.WithUniqueName("BroadcastInit.cs.txt")
					.Replace("{{BODY}}", BodyWith(interfaces));

				context.AddSource("Initialization.g.cs", text);
			}

		private static string BodyWith
			(
				IEnumerable<INamedTypeSymbol> interfaces
			)
			{
				var builder = new StringBuilder();

				foreach (var @interface in interfaces)
					{
						builder.AppendLine(
							$"{Formatting.Tab(3)}BroadcastRegistry.Add<{@interface}>(x => new {BroadcastName(@interface)}(x));");
					}

				return builder.ToString();
			}

		private static string BroadcastName
			(
				INamedTypeSymbol @interface
			)
			=> @interface.Name + "Broadcast";
	}