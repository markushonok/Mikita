using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Mikita.Generation.Live;

public interface ILivePlan
	{
		ImmutableArray<ILiveRelay> Relays { get; }

		ImmutableArray<Diagnostic> Diagnostics { get; }

		bool IsValid { get; }
	}