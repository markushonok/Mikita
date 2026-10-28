using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Mikita.Generation.Manifests;

public interface IImplementationManifest
	{
		string Path { get; }

		ImmutableArray<IImplementationEntry> Entries { get; }

		ImmutableArray<Diagnostic> Diagnostics { get; }

		bool IsValid { get; }
	}