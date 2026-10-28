using Microsoft.CodeAnalysis;

namespace Mikita.Generation.Live;

public interface ILiveRelay
	{
		INamedTypeSymbol Type { get; }

		string Name { get; }

		string Declaration { get; }

		string Registration { get; }
	}