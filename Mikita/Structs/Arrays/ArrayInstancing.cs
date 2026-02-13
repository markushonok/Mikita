using Mikita.Math.Sizes;

namespace Mikita.Structs.Arrays;

public static class ArrayInstancing
	{
		extension(Array)
			{
				public static Array2D<T?> NewOf<T>
					(
						ISize2D<int> size
					)
					{
						var source = new T?[size.X * size.Y];
						return new Array2D<T?>(source, size);
					}
			}
	}