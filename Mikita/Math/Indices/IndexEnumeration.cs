using System.Collections.Generic;

namespace Mikita.Math.Indices;

public static class IndexEnumeration
	{
		public static IEnumerable<Index2D> Indices<T>
			(
				this T[,] array
			)
			{
				for (var x = 0; x < array.GetLength(0); x++)
				for (var y = 0; y < array.GetLength(1); y++)
					yield return Index.Of(x, y);
			}
	}