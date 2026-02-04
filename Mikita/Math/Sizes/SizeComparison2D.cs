using Mikita.Objects;
using System.Numerics;

namespace Mikita.Math.Sizes;

public static class SizeComparison2D
	{
		extension<T>(ISize2D<T> size)
			where T: INumber<T>
			{
				public bool IsNotZero
					=> size.NotEquals(Size2D<T>.Zero);

				public bool IsZero
					=> size.Equals(Size2D<T>.Zero);
			}
	}