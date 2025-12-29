using Mikita.Generation.Inference.Summary.Nodes;
using Mikita.Generation.Inference.Targets;
using Mikita.Generation.Targets;

namespace Mikita.Generation.Broadcasting;

public static class BroadcastRoot
	{
		public static SummaryNode Instance
			=> new
				(
					"global::Mikita.Observation.Broadcast.To",
					TypeParameters: ["T"],
					Targets: [TypeTarget.With(["T"], TargetProducts.Broadcast)]
				);
	}