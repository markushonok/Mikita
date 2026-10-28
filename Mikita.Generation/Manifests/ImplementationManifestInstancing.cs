using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Mikita.Generation.Manifests;

public static class ImplementationManifestInstancing
	{
		public const string FileName
			= "Implementations.toml";

		public static IImplementationManifest Read
			(
				AdditionalText text,
				CancellationToken cancel
			)
			{
				var source = text.GetText(cancel);

				return source is null
					? Unreadable(text.Path)
					: new Reader(source, LinesOf(source), text.Path).Read();
			}

		private static ImmutableArray<string> LinesOf(SourceText source)
			=> source
				.Lines
				.Select(x => x.ToString())
				.ToImmutableArray();

		private static IImplementationManifest Unreadable(string path)
			=> new ImplementationManifest
				(
					path,
					ImmutableArray<IImplementationEntry>.Empty,
					ImmutableArray.Create
						(
							Diagnostic.Create
								(
									ManifestDiagnostics.ManifestUnreadable,
									Location.None,
									"the file cannot be read"
								)
						)
				);

		private sealed class Reader
			(
				SourceText source,
				ImmutableArray<string> lines,
				string path
			)
			{
				public IImplementationManifest Read()
					{
						for (var index = 0; index < lines.Length; index++)
							ReadLine(index);

						Complete();
						return Manifest();
					}

				private void ReadLine(int index)
					{
						var text = WithoutComment(lines[index]).Trim();

						if (text.Length == 0)
							return;

						if (text == TableHeader)
							{
								StartEntry();
							}
						else AssignText(index, text);
					}

				private void StartEntry()
					{
						Complete();
						currentType = null;
						currentFeatures = ImmutableArray.CreateBuilder<string>();
					}

				private void Complete()
					{
						if (currentFeatures is null)
							return;

						if (currentType is null)
							{
								Invalid("an implementation needs a 'type' string");
							}
						else entries.Add
							(
								new ImplementationEntry
									(
										currentType,
										currentFeatures.ToImmutable()
									)
							);

						currentType = null;
						currentFeatures = null;
					}

				private void AssignText(int index, string text)
					{
						var separator = text.IndexOf('=');

						if (separator < 0)
							{
								Error(index, "expected 'key = value'");
								return;
							}

						var key = text.Substring(0, separator).Trim();
						var value = text.Substring(separator + 1).Trim();
						Assign(index, key, value);
					}

				private void Assign(int index, string key, string value)
					{
						if (currentFeatures is null)
							{
								AssignRoot(index, key, value);
							}
						else AssignEntry(index, key, value);
					}

				private void AssignRoot(int index, string key, string value)
					{
						if (key != "version")
							{
								Error(index, $"the key '{key}' is unknown");
								return;
							}

						ReadVersion(index, value);
					}

				private void ReadVersion(int index, string value)
					{
						if (hasVersion)
							{
								Error(index, "the 'version' key is repeated");
								return;
							}

						hasVersion = true;
						versionLine = index;

						if (!int.TryParse(value, out var number))
							{
								Error
									(
										index,
										$"the supported version is {SupportedVersion}"
									);
							}
						else version = number;
					}

				private void AssignEntry
					(
						int index,
						string key,
						string value
					)
					{
						if (key == "type")
							{
								ReadType(index, value);
							}
						else if (key == "features")
							{
								ReadFeatures(index, value);
							}
						else Error(index, $"the key '{key}' is unknown");
					}

				private void ReadType(int index, string value)
					{
						if (currentType is not null)
							{
								Error(index, "the 'type' key is repeated");
								return;
							}

						currentType = ReadString(index, value);
					}

				private void ReadFeatures(int index, string value)
					{
						if (!IsArray(value))
							{
								Error
									(
										index,
										"the 'features' value must be an array"
										+ " of strings"
									);
								return;
							}

						var body = value
							.Substring(1, value.Length - 2)
							.Trim();

						if (body.Length > 0)
							ReadFeatureItems(index, body);
					}

				private void ReadFeatureItems(int index, string body)
					{
						foreach (var raw in body.Split(','))
							{
								var name = ReadString(index, raw.Trim());

								if (name is null)
									return;

								if (!Feature.Known(name))
									{
										Error
											(
												index,
												$"the feature '{name}' is unknown"
											);
										return;
									}

								currentFeatures!.Add(name);
							}
					}

				private string? ReadString(int index, string value)
					{
						if (!IsQuoted(value))
							{
								Error(index, "expected a quoted string");
								return null;
							}

						var body = value.Substring(1, value.Length - 2);

						if (body.IndexOf('"') < 0 && body.IndexOf('\\') < 0)
							return body;

						Error(index, "a string cannot contain '\"' or '\\'");
						return null;
					}

				private IImplementationManifest Manifest()
					{
						var manifest = new ImplementationManifest
							(
								path,
								entries.ToImmutable(),
								diagnostics.ToImmutable()
							);

						return manifest.IsValid && version != SupportedVersion
							? VersionError(manifest)
							: manifest;
					}

				private IImplementationManifest VersionError
					(
						IImplementationManifest manifest
					)
					=> new ImplementationManifest
						(
							manifest.Path,
							ImmutableArray<IImplementationEntry>.Empty,
							ImmutableArray.Create
								(
									hasVersion
										? VersionMismatch()
										: VersionMissing()
								)
						);

				private Diagnostic VersionMismatch()
					=> Diagnostic.Create
						(
							ManifestDiagnostics.ManifestInvalid,
							At(versionLine),
							$"the supported version is {SupportedVersion}"
						);

				private static Diagnostic VersionMissing()
					=> Diagnostic.Create
						(
							ManifestDiagnostics.ManifestInvalid,
							Location.None,
							"the 'version' key is missing"
						);

				private void Error(int index, string message)
					=> diagnostics.Add
						(
							Diagnostic.Create
								(
									ManifestDiagnostics.ManifestInvalid,
									At(index),
									message
								)
						);

				private void Invalid(string message)
					=> diagnostics.Add
						(
							Diagnostic.Create
								(
									ManifestDiagnostics.ManifestInvalid,
									Location.None,
									message
								)
						);

				private Location At(int line)
					{
						var index = Math.Max
							(
								0,
								Math.Min(line, source.Lines.Count - 1)
							);
						var textLine = source.Lines[index];
						var position = new LinePosition
							(
								index,
								textLine.Span.Length
							);

						return Location.Create
							(
								path,
								new TextSpan(textLine.End, 0),
								new LinePositionSpan(position, position)
							);
					}

				private static string WithoutComment(string line)
					{
						var quoted = false;

						for (var index = 0; index < line.Length; index++)
							{
								if (line[index] == '"'
									&& (index == 0 || line[index - 1] != '\\'))
									quoted = !quoted;

								if (line[index] == '#' && !quoted)
									return line.Substring(0, index);
							}

						return line;
					}

				private static bool IsQuoted(string value)
					=> value.Length >= 2
						&& value[0] == '"'
						&& value[value.Length - 1] == '"';

				private static bool IsArray(string value)
					=> value.Length >= 2
						&& value[0] == '['
						&& value[value.Length - 1] == ']';

				private readonly ImmutableArray<IImplementationEntry>.Builder entries
					= ImmutableArray.CreateBuilder<IImplementationEntry>();

				private readonly ImmutableArray<Diagnostic>.Builder diagnostics
					= ImmutableArray.CreateBuilder<Diagnostic>();

				private string? currentType;

				private ImmutableArray<string>.Builder? currentFeatures;

				private int version;

				private int versionLine;

				private bool hasVersion;

				private const int SupportedVersion = 1;

				private const string TableHeader
					= "[[implementation]]";
			}
	}