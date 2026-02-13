using Mikita.Math.Sizes;
using System.Collections.Generic;

namespace Mikita.Math.Indices;

public static class Index
	{
		public static Index2D Of(int x, int y)
			=> new(x, y);

		public static IEnumerable<IIndex2D> EachIn
			(
				ISize2D<int> size
			)
			{
				for (var y = 0; y < size.Y; y++)
				for (var x = 0; x < size.X; x++)
					yield return Index.Of(x, y);
			}

		extension(IIndex2D index)
			{
				public int InlineTo(ISize2D<int> size)
				=> index.Y * size.X + index.X;
			}
	}