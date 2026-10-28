using System.Collections.Immutable;

namespace Mikita.Generation.Manifests;

public sealed class ImplementationEntry
	(
		string type,
		ImmutableArray<string> features
	)
	: IImplementationEntry
	{
		public string Type
			=> type;

		public ImmutableArray<string> Features
			=> features;

		public bool Supports(string feature)
			=> features.Contains(feature);
	}