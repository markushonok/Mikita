using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Mikita.Generation.Live;

public sealed class LivePlan
	(
		ImmutableArray<ILiveRelay> relays,
		ImmutableArray<Diagnostic> diagnostics
	)
	: ILivePlan
	{
		public ImmutableArray<ILiveRelay> Relays
			=> relays;

		public ImmutableArray<Diagnostic> Diagnostics
			=> diagnostics;

		public bool IsValid
			=> diagnostics.IsEmpty;
	}