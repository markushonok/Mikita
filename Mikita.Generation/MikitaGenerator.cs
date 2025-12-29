using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mikita.Generation.Broadcasting;
using Mikita.Generation.Inference.Targets;
using Mikita.Generation.Targets;
using System;
using System.Collections.Generic;

namespace Mikita.Generation;

[Generator]
public sealed class MikitaGenerator
	: IIncrementalGenerator
	{
		public void Initialize
			(
				IncrementalGeneratorInitializationContext context
			)
			{
				var configInterfaces = context.AdditionalTextsProvider
					.Where(static file => file.Path.EndsWith("Mikita.Generation.json", StringComparison.OrdinalIgnoreCase))
					.Combine(context.CompilationProvider)
					.SelectMany< (AdditionalText Left, Compilation Right), INamedTypeSymbol >
						(
							static (pair, _) =>
								{
									var (additionalText, compilation) = pair;
									var text = additionalText.GetText();

									if (text is null)
										return [];

									var json = text.ToString();
									var targets = TargetType.FromJson(json);

									if (targets.Count == 0)
										return [];

									var result = new List<INamedTypeSymbol>();

									foreach (var target in targets)
										{
											if (string.IsNullOrWhiteSpace(target.Name))
												continue;

											// "global::Ns.Type" → "Ns.Type" для GetTypeByMetadataName
											var typeName = target.Name.StartsWith("global::", StringComparison.Ordinal)
												? target.Name.Substring("global::".Length)
												: target.Name;

											var symbol = compilation.GetTypeByMetadataName(typeName);
											if (symbol is not INamedTypeSymbol)
												continue;

											if (symbol.Arity != target.Arity)
												continue;

											// Пока поддерживаем только Broadcast
											if (!target.Products.HasFlag(TargetProducts.Broadcast))
												continue;

											// Генерим только для интерфейсов
											if (symbol.TypeKind != TypeKind.Interface)
												continue;

											result.Add(symbol);
										}

									return result;
								}
						)
					.Collect();

				context.RegisterSourceOutput
					(
						configInterfaces,
						static (spc, interfaces)
							=> Broadcast.AddSourceFiles(spc, interfaces)
					);
			}
	}