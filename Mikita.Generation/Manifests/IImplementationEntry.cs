using System.Collections.Immutable;

namespace Mikita.Generation.Manifests;

public interface IImplementationEntry
	{
		string Type { get; }

		ImmutableArray<string> Features { get; }

		bool Supports(string feature);
	}