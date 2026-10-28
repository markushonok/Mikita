using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Mikita.Generation.Manifests;

public sealed class ImplementationManifest
	(
		string path,
		ImmutableArray<IImplementationEntry> entries,
		ImmutableArray<Diagnostic> diagnostics
	)
	: IImplementationManifest
	{
		public string Path
			=> path;

		public ImmutableArray<IImplementationEntry> Entries
			=> entries;

		public ImmutableArray<Diagnostic> Diagnostics
			=> diagnostics;

		public bool IsValid
			=> diagnostics.IsEmpty;
	}