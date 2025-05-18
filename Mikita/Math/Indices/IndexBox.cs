using Mikita.Math.Sizes;
using System.Collections.Generic;

namespace Mikita.Math.Indices;

public static class IndexBox
	{
		public static IEnumerable<Index2D> With
			(
				IIndex2D position,
				ISize2D<int> size
			)
			{
				for (var x = position.X; x < position.X + size.X; x++)
				for (var y = position.Y; y < position.Y + size.Y; y++)
					yield return Index.Of(x, y);
			}
	}