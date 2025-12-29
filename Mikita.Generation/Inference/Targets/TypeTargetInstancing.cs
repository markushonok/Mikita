using Mikita.Generation.Inference.Types.Trees;
using Mikita.Generation.Targets;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Generation.Inference.Targets;

public static class TypeTargetInstancing
	{
		extension(TypeTarget)
			{
				public static TypeTarget With
					(
						IEnumerable<string> typeParameters,
						TargetProducts products
					)
					=> new
						(
							typeParameters,
							typeParameters.Select(TypeNode.With),
							products
						);
			}
	}