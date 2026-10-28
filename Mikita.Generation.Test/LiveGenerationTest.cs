using Mikita.Generation.Live;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Mikita.Evaluation.Liveness;
using System.Collections.Immutable;

namespace Mikita.Generation.Test;

public static class LiveGenerationTest
	{
		[Fact]
		public static void RelayImplementsTheInterface()
			{
				const string source
					= """
						namespace Sample;

						public interface ICounter
							{
								int Value { get; set; }

								string Describe(string suffix);
							}
						""";

				var result = Run(source, Manifest("Sample.ICounter", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain("internal sealed class Live_Sample_ICounter");
				generated.ShouldContain(": global::Sample.ICounter");
				generated.ShouldContain
					(
						"get => ((global::Sample.ICounter) function())"
						+ ".Value;"
					);
				generated.ShouldContain
					(
						"set => ((global::Sample.ICounter) function())"
						+ ".Value = value;"
					);
				generated.ShouldContain
					(
						"((global::Sample.ICounter) function())"
						+ ".Describe(suffix)"
					);
				generated.ShouldContain("global::Sample.ICounter.Value");
				generated.ShouldContain("global::Sample.ICounter.Describe");
				generated.ShouldContain("LiveRelay<global::Sample.ICounter>.Factory");

				Errors(result.Output).ShouldBeEmpty();
				Errors(result.Diagnostics).ShouldBeEmpty();
			}


		[Fact]
		public static void ConflictingReturnsUseExplicitImplementation()
			{
				const string source
					= """
					  namespace Sample;

					  public interface IItem {}

					  public interface IItemList
					  	: System.Collections.Generic.IEnumerable<IItem> {}
					  """;

				var result = Run(source, Manifest("Sample.IItemList", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain
					(
						"global::System.Collections.IEnumerable.GetEnumerator"
					);

				Errors(result.Output).ShouldBeEmpty();
			}

		[Fact]
		public static void ForeignTypeIsReported()
			{
				const string source = "namespace Sample; public interface ICounter {}";

				var result = Run(source, Manifest("System.IDisposable", "live"));

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0005");
				result.Generated.ShouldBeNull();
			}

		[Fact]
		public static void ClassIsReported()
			{
				const string source
					= "namespace Sample; public sealed class Counter {}";

				var result = Run(source, Manifest("Sample.Counter", "live"));

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0007");
				result.Generated.ShouldBeNull();
			}


		[Fact]
		public static void EventIsRelayed()
			{
				const string source
					= """
						namespace Sample;

						public interface IAnnouncer
							{
								event System.EventHandler? Changed;
							}
						""";

				var result = Run(source, Manifest("Sample.IAnnouncer", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain
					(
						"event global::System.EventHandler? "
						+ "global::Sample.IAnnouncer.Changed"
					);

				Errors(result.Output).ShouldBeEmpty();
			}

		[Fact]
		public static void IndexerIsRelayed()
			{
				const string source
					= """
						namespace Sample;

						public interface IIndexed
							{
								string this[int index] { get; set; }
							}
						""";

				var result = Run(source, Manifest("Sample.IIndexed", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain("global::Sample.IIndexed.this[int index]");
				Errors(result.Output).ShouldBeEmpty();
			}

		[Fact]
		public static void GenericMethodIsRelayed()
			{
				const string source
					= """
						namespace Sample;

						public interface IConverter
							{
								TResult Convert<TResult>(object value)
									where TResult: class;
							}
						""";

				var result = Run(source, Manifest("Sample.IConverter", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain("global::Sample.IConverter.Convert");
				Errors(result.Output).ShouldBeEmpty();
			}

		[Fact]
		public static void InheritedMembersAreRelayed()
			{
				const string source
					= """
						namespace Sample;

						public interface IBase
							{
								int Count { get; }
							}

						public interface IDerived: IBase
							{
								void Run();
							}
						""";

				var result = Run(source, Manifest("Sample.IDerived", "live"));
				var generated = result.Generated.ShouldNotBeNull();

				generated.ShouldContain("global::Sample.IBase.Count");
				generated.ShouldContain("global::Sample.IDerived.Run");
				Errors(result.Output).ShouldBeEmpty();
			}

		[Fact]
		public static void InitOnlySetterIsReported()
			{
				const string source
					= """
						namespace Sample;

						public interface IValue
							{
								int Value { get; init; }
							}
						""";

				var result = Run(source, Manifest("Sample.IValue", "live"));

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0008");
				result.Generated.ShouldBeNull();
			}

		[Fact]
		public static void UnknownTypeIsReported()
			{
				const string source = "namespace Sample; public interface ICounter {}";

				var result = Run(source, Manifest("Sample.Missing", "live"));

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0004");
				result.Generated.ShouldBeNull();
			}

		[Fact]
		public static void DuplicateTypeIsReported()
			{
				const string source = "namespace Sample; public interface ICounter {}";
				var manifest
					= """
						version = 1

						[[implementation]]
						type = "Sample.ICounter"
						features = ["live"]

						[[implementation]]
						type = "Sample.ICounter"
						features = ["live"]
						""";

				var result = Run(source, manifest);

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0006");
				result.Generated.ShouldBeNull();
			}

		[Fact]
		public static void MultipleManifestsAreReported()
			{
				const string source = "namespace Sample; public interface ICounter {}";
				var manifest = Manifest("Sample.ICounter", "live");

				var result = Run(source, manifest, manifest);

				DiagnosticIds(result.Diagnostics).ShouldContain("MIK0003");
				result.Generated.ShouldBeNull();
			}

		private static Generation Run(string source, params string[] manifests)
			{
				var options = new CSharpParseOptions(LanguageVersion.Latest);

				var compilation = CSharpCompilation.Create
					(
						"TestAssembly",
						[CSharpSyntaxTree.ParseText(source, options)],
						References,
						new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
					);

				GeneratorDriver driver = CSharpGeneratorDriver.Create
					(
						[new LiveGenerator().AsSourceGenerator()],
						additionalTexts: manifests
							.Select(x => new ManifestText(x))
							.ToArray(),
						parseOptions: options
					);

				driver = driver.RunGeneratorsAndUpdateCompilation
					(
						compilation,
						out var output,
						out var diagnostics
					);

				var result = driver.GetRunResult();
				var generated = result
					.Results
					.SelectMany(x => x.GeneratedSources)
					.Select(x => x.SourceText.ToString())
					.FirstOrDefault();

				return new Generation(diagnostics, output, generated);
			}

		private static string Manifest(string type, string feature)
			=> $"""
				version = 1

				[[implementation]]
				type = "{type}"
				features = ["{feature}"]
				""";

		private static ImmutableArray<Diagnostic> Errors(Compilation compilation)
			=> Errors(compilation.GetDiagnostics());

		private static ImmutableArray<Diagnostic> Errors
			(
				ImmutableArray<Diagnostic> diagnostics
			)
			=> diagnostics
				.Where(x => x.Severity == DiagnosticSeverity.Error)
				.ToImmutableArray();

		private static ImmutableArray<string> DiagnosticIds
			(
				ImmutableArray<Diagnostic> diagnostics
			)
			=> diagnostics
				.Select(x => x.Id)
				.ToImmutableArray();

		private sealed record Generation
			(
				ImmutableArray<Diagnostic> Diagnostics,
				Compilation Output,
				string? Generated
			);

		private static readonly ImmutableArray<MetadataReference> References
			= BuildReferences();

		private static ImmutableArray<MetadataReference> BuildReferences()
			{
				var references = ImmutableArray.CreateBuilder<MetadataReference>();

				references.AddRange
					(
						((string) AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
							.Split(Path.PathSeparator)
							.Select(path => MetadataReference.CreateFromFile(path))
					);

				references.Add
					(
						MetadataReference.CreateFromFile
							(
								typeof(LiveRelay<>).Assembly.Location
							)
					);

				return references.ToImmutable();
			}
	}