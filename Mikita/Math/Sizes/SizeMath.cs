using System.Numerics;

namespace Mikita.Math.Sizes;

public static class SizeMath
	{
		extension<T>(Size) where T: INumber<T>
			{
				public static Size2D<T> IntersectionOf
					(
						ISize2D<T> a,
						ISize2D<T> b
					)
					=> Size.Of
						(
							x: T.Min(a.X, b.X),
							y: T.Min(a.Y, b.Y)
						);
			}
	}