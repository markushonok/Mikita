using Mikita.Generation.Manifests;
using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace Mikita.Generation.Live;

[Generator(LanguageNames.CSharp)]
public sealed class LiveGenerator
	: IIncrementalGenerator
	{
		public void Initialize
			(
				IncrementalGeneratorInitializationContext context
			)
			{
				var manifests = context
					.AdditionalTextsProvider
					.Where(IsManifest)
					.Select(ImplementationManifestInstancing.Read);

				var plans = manifests
					.Collect()
					.Combine(context.CompilationProvider)
					.Select
						(
							(pair, cancel) => LivePlanInstancing.From
								(
									pair.Left,
									pair.Right,
									cancel
								)
						);

				context.RegisterSourceOutput(plans, Produce);
			}

		private static bool IsManifest(AdditionalText text)
			=> string.Equals
				(
					Path.GetFileName(text.Path),
					ImplementationManifestInstancing.FileName,
					StringComparison.Ordinal
				);

		private static void Produce
			(
				SourceProductionContext context,
				ILivePlan plan
			)
			{
				foreach (var diagnostic in plan.Diagnostics)
					context.ReportDiagnostic(diagnostic);

				if (plan.IsValid && plan.Relays.Length > 0)
					context.AddSource
						(
							"Live.g.cs",
							new LiveSource(plan.Relays).Text
						);
			}
	}