using Mikita.Generation.Broadcasting;
using Mikita.Generation.Inference.Summary.Nodes;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Roots;

public static class Root
	{
		public static IReadOnlyList<ISummaryNode> List
			=> [BroadcastRoot.Instance];
	}