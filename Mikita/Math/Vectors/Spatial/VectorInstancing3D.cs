using System.Numerics;

namespace Mikita.Math.Vectors.Spatial;

public static class VectorInstancing3D
	{
		extension<T>(Vector3D<T>)
			where T: INumber<T>
			{
				public static Vector3D<T> Zero
					=> Vector3D.Of(T.Zero);
			}

		extension(Vector3D)
			{
				public static Vector3D<T> Of<T>(T number)
					=> Vector.PointingTo(number, number, number);
			}
	}