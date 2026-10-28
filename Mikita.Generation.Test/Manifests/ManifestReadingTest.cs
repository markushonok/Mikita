using Mikita.Generation.Manifests;
using System.Collections.Immutable;

namespace Mikita.Generation.Test.Manifests;

public static class ManifestReadingTest
	{
		[Fact]
		public static void KnownFeatureIsRead()
			{
				var manifest = Read(Manifest("Sample.ICounter", "live"));

				manifest.IsValid.ShouldBeTrue();
				manifest.Entries.Length.ShouldBe(1);
				manifest.Entries[0].Type.ShouldBe("Sample.ICounter");
				manifest.Entries[0].Features.ShouldBe(new[] { "live" });
			}

		[Fact]
		public static void CommentIsIgnored()
			{
				var manifest = Read
					(
						"""
						version = 1 # manifest version

						[[implementation]]
						type = "Sample.ICounter" # the type
						features = ["live"]
						"""
					);

				manifest.IsValid.ShouldBeTrue();
				manifest.Entries[0].Type.ShouldBe("Sample.ICounter");
			}

		[Fact]
		public static void UnknownFeatureIsReported()
			{
				var manifest = Read(Manifest("Sample.ICounter", "emit"));

				DiagnosticIds(manifest).ShouldContain("MIK0002");
				manifest.IsValid.ShouldBeFalse();
			}

		[Fact]
		public static void MissingVersionIsReported()
			{
				var manifest = Read
					(
						"""
						[[implementation]]
						type = "Sample.ICounter"
						features = ["live"]
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		[Fact]
		public static void WrongVersionIsReported()
			{
				var manifest = Read
					(
						"""
						version = 2

						[[implementation]]
						type = "Sample.ICounter"
						features = ["live"]
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		[Fact]
		public static void RepeatedVersionIsReported()
			{
				var manifest = Read
					(
						"""
						version = 1
						version = 1
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		[Fact]
		public static void UnknownKeyIsReported()
			{
				var manifest = Read
					(
						"""
						version = 1
						extra = 1

						[[implementation]]
						type = "Sample.ICounter"
						features = ["live"]
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		[Fact]
		public static void MissingTypeIsReported()
			{
				var manifest = Read
					(
						"""
						version = 1

						[[implementation]]
						features = ["live"]
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		[Fact]
		public static void NonArrayFeaturesAreReported()
			{
				var manifest = Read
					(
						"""
						version = 1

						[[implementation]]
						type = "Sample.ICounter"
						features = "live"
						"""
					);

				DiagnosticIds(manifest).ShouldContain("MIK0002");
			}

		private static IImplementationManifest Read(string text)
			=> ImplementationManifestInstancing.Read
				(
					new ManifestText(text),
					CancellationToken.None
				);

		private static ImmutableArray<string> DiagnosticIds
			(
				IImplementationManifest manifest
			)
			=> manifest
				.Diagnostics
				.Select(x => x.Id)
				.ToImmutableArray();

		private static string Manifest(string type, string feature)
			=> $"""
				version = 1

				[[implementation]]
				type = "{type}"
				features = ["{feature}"]
				""";
	}