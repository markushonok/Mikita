using Mikita.Math.Vectors.Planar;
using System.Numerics;

namespace Mikita.Math.Sizes;

public static class SizeConversion2D
	{
		extension<T>(ISize2D<T> size)
			{
				public Vector2D<T> AsVector
					=> new(size.X, size.Y);
			}

		extension<T>(ISize2D<T> size)
			where T: IFloatingPoint<T>
			{
				public Size2D<int> ToInt
					=> Size.Of
						(
							x: T.ConvertToInteger<int>(size.X),
							y: T.ConvertToInteger<int>(size.Y)
						);
			}
	}