using System.Numerics;

namespace Mikita.Math.Sizes;

public static class SizeInstancing
	{
		extension(Size)
			{
				public static Size2D<T> Of<T>(T x, T y)
					=> new(x, y);
			}

		extension(Size2D)
			{
				public static Size2D<T> Of<T>(T width)
					=> new(x: width, y: width);
			}

		extension<T>(Size2D<T>)
			where T: INumber<T>
			{
				public static Size2D<T> Zero
					=> new(x: T.Zero, y: T.Zero);
			}
	}