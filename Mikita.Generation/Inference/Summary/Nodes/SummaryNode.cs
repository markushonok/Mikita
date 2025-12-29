using Mikita.Generation.Inference.Targets;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Summary.Nodes;

public sealed record SummaryNode
	(
		string Name,
		IEnumerable<string> TypeParameters,
		IEnumerable<ITypeTarget> Targets
	)
	: ISummaryNode;