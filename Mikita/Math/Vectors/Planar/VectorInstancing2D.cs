using System.Numerics;

namespace Mikita.Math.Vectors.Planar;

public static class VectorInstancing2D
	{
		extension<T>(Vector2D<T>)
			where T: INumber<T>
			{
				public static Vector2D<T> Zero
					=> Vector.PointingTo(T.Zero, T.Zero);
			}
	}