using Mikita.Generation.Manifests;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mikita.Generation.Live;

public static class LivePlanInstancing
	{
		public static ILivePlan From
			(
				ImmutableArray<IImplementationManifest> manifests,
				Compilation compilation,
				CancellationToken cancel
			)
			{
				if (manifests.Length > 1)
					return Multiple(manifests);

				if (manifests.Length == 0)
					return Empty;

				var manifest = manifests[0];

				if (!manifest.IsValid)
					return WithoutRelays(manifest.Diagnostics);

				var requirements = Requirements(compilation);

				return requirements.IsEmpty
					? Resolve(manifest, compilation, cancel)
					: WithoutRelays(requirements);
			}

		private static ILivePlan Resolve
			(
				IImplementationManifest manifest,
				Compilation compilation,
				CancellationToken cancel
			)
			{
				var entries = LiveEntries(manifest);

				if (entries.IsEmpty)
					return Empty;

				var diagnostics
					= ImmutableArray.CreateBuilder<Diagnostic>();
				var symbols = Symbols(entries, compilation, cancel);
				var relays = Relays
					(
						symbols,
						entries,
						compilation,
						diagnostics
					);

				return new LivePlan
					(
						relays,
						diagnostics.ToImmutable()
					);
			}

		private static ImmutableArray<IImplementationEntry> LiveEntries
			(
				IImplementationManifest manifest
			)
			=> manifest
				.Entries
				.Where(x => x.Supports(Feature.Live))
				.ToImmutableArray();

		private static ImmutableArray<ILiveRelay> Relays
			(
				ImmutableArray<INamedTypeSymbol?> symbols,
				ImmutableArray<IImplementationEntry> entries,
				Compilation compilation,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				var relays = ImmutableArray.CreateBuilder<ILiveRelay>();

				for (var index = 0; index < symbols.Length; index++)
					{
						var relay = Relay
							(
								symbols[index],
								entries[index],
								compilation,
								relays,
								diagnostics
							);

						if (relay is not null)
							relays.Add(relay);
					}

				return relays.ToImmutable();
			}

		private static ILiveRelay? Relay
			(
				INamedTypeSymbol? symbol,
				IImplementationEntry entry,
				Compilation compilation,
				ImmutableArray<ILiveRelay>.Builder relays,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				if (symbol is null)
					return Unresolved(entry, diagnostics);

				if (symbol.TypeKind != TypeKind.Interface)
					return NotInterface(entry, diagnostics);

				if (!Owned(symbol, compilation))
					return Foreign(symbol, entry, diagnostics);

				if (relays.Any(x => Same(x.Type, symbol)))
					return Duplicated(entry, diagnostics);

				return LiveRelayInstancing.Of(symbol, diagnostics);
			}

		private static bool Owned
			(
				ITypeSymbol symbol,
				Compilation compilation
			)
			=> DeclaredHere(symbol, compilation)
			   || ConstructedHere(symbol, compilation);

		private static bool DeclaredHere
			(
				ITypeSymbol symbol,
				Compilation compilation
			)
			=> symbol.ContainingAssembly is {} assembly
				&& assembly.Identity.Equals(compilation.Assembly.Identity);

		private static bool ConstructedHere
			(
				ITypeSymbol symbol,
				Compilation compilation
			)
			=> symbol is INamedTypeSymbol {IsGenericType: true} named
			   && named.TypeArguments.Any
				   (
					   x => Owned(x, compilation)
				   );

		private static bool Same
			(
				INamedTypeSymbol left,
				INamedTypeSymbol right
			)
			=> SymbolEqualityComparer
				.Default
				.Equals(left, right);

		private static ILiveRelay? Unresolved
			(
				IImplementationEntry entry,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				diagnostics.Add
					(
						Diagnostic.Create
							(
								ManifestDiagnostics.TypeUnknown,
								Location.None,
								entry.Type
							)
					);

				return null;
			}

		private static ILiveRelay? NotInterface
			(
				IImplementationEntry entry,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				diagnostics.Add
					(
						Diagnostic.Create
							(
								LiveDiagnostics.LiveRequiresInterface,
								Location.None,
								entry.Type
							)
					);

				return null;
			}

		private static ILiveRelay? Foreign
			(
				INamedTypeSymbol symbol,
				IImplementationEntry entry,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				diagnostics.Add
					(
						Diagnostic.Create
							(
								ManifestDiagnostics.TypeNotDeclaredHere,
								Location.None,
								entry.Type,
								symbol.ContainingAssembly?.Name ?? "unknown"
							)
					);

				return null;
			}

		private static ILiveRelay? Duplicated
			(
				IImplementationEntry entry,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				diagnostics.Add
					(
						Diagnostic.Create
							(
								ManifestDiagnostics.TypeDuplicated,
								Location.None,
								entry.Type
							)
					);

				return null;
			}

		private static ImmutableArray<Diagnostic> Requirements
			(
				Compilation compilation
			)
			{
				var diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

				if (compilation.GetTypeByMetadataName(RelayRegistry) is null)
					diagnostics.Add(Missing(LiveDiagnostics.LiveRelayMissing));

				if (compilation.GetTypeByMetadataName(ModuleInitializer) is null)
					diagnostics.Add(Missing(LiveDiagnostics.ModuleInitializerMissing));

				return diagnostics.ToImmutable();
			}

		private static Diagnostic Missing
			(
				DiagnosticDescriptor descriptor
			)
			=> Diagnostic.Create(descriptor, Location.None);

		private static ILivePlan Multiple
			(
				ImmutableArray<IImplementationManifest> manifests
			)
			=> WithoutRelays
				(
					ImmutableArray.Create
						(
							Diagnostic.Create
								(
									ManifestDiagnostics.MultipleManifests,
									Location.None,
									string.Join
										(
											", ",
											manifests.Select(x => x.Path)
										)
								)
						)
				);

		private static ILivePlan WithoutRelays
			(
				ImmutableArray<Diagnostic> diagnostics
			)
			=> new LivePlan
				(
					ImmutableArray<ILiveRelay>.Empty,
					diagnostics
				);

		private static ImmutableArray<INamedTypeSymbol?> Symbols
			(
				ImmutableArray<IImplementationEntry> entries,
				Compilation compilation,
				CancellationToken cancel
			)
			{
				var tree = Tree(entries, compilation, cancel);
				var model = compilation
					.AddSyntaxTrees(tree)
					.GetSemanticModel(tree);
				var classes = ClassesOf(tree, cancel);
				var symbols
					= ImmutableArray.CreateBuilder<INamedTypeSymbol?>(entries.Length);

				for (var index = 0; index < entries.Length; index++)
					symbols.Add(SymbolOf(model, classes, index, cancel));

				return symbols.ToImmutable();
			}

		private static SyntaxTree Tree
			(
				ImmutableArray<IImplementationEntry> entries,
				Compilation compilation,
				CancellationToken cancel
			)
			{
				var text = new StringBuilder();

				for (var index = 0; index < entries.Length; index++)
					text
						.Append("internal sealed class LiveType")
						.Append(index)
						.Append(" { internal ")
						.Append(entries[index].Type)
						.Append(" Value { get; } }\n");

				return CSharpSyntaxTree.ParseText
					(
						text.ToString(),
						OptionsOf(compilation),
						path: "Implementations.g.cs",
						cancellationToken: cancel
					);
			}

		private static CSharpParseOptions OptionsOf
			(
				Compilation compilation
			)
			=> compilation.SyntaxTrees.FirstOrDefault()?.Options
				as CSharpParseOptions
				?? CSharpParseOptions.Default;

		private static ImmutableDictionary<string, ClassDeclarationSyntax> ClassesOf
			(
				SyntaxTree tree,
				CancellationToken cancel
			)
			=> tree
				.GetRoot(cancel)
				.DescendantNodes()
				.OfType<ClassDeclarationSyntax>()
				.ToImmutableDictionary
					(
						x => x.Identifier.Text,
						StringComparer.Ordinal
					);

		private static INamedTypeSymbol? SymbolOf
			(
				SemanticModel model,
				ImmutableDictionary<string, ClassDeclarationSyntax> classes,
				int index,
				CancellationToken cancel
			)
			{
				if (!classes.TryGetValue("LiveType" + index, out var type))
					return null;

				var property = type
					.Members
					.OfType<PropertyDeclarationSyntax>()
					.FirstOrDefault();

				if (property is null)
					return null;

				var symbol = model
					.GetTypeInfo(property.Type, cancel)
					.Type as INamedTypeSymbol;

				return symbol is { TypeKind: TypeKind.Error }
					? null
					: symbol;
			}

		private const string RelayRegistry
			= "Mikita.Evaluation.Liveness.LiveRelay`1";

		private const string ModuleInitializer
			= "System.Runtime.CompilerServices.ModuleInitializerAttribute";

		private static readonly ILivePlan Empty
			= new LivePlan
				(
					ImmutableArray<ILiveRelay>.Empty,
					ImmutableArray<Diagnostic>.Empty
				);
	}