using Mikita.Objects;
using System.Numerics;

namespace Mikita.Math.Vectors.Spatial;

public static class VectorComparison3D
	{
		extension<T>(IVector3D<T> vector)
			where T: INumber<T>
			{
				public bool IsNotZero
					=> vector.NotEquals(Vector3D<T>.Zero);

				public bool IsZero
					=> vector.Equals(Vector3D<T>.Zero);
			}

		extension<T>(IVector3D<T>)
			{
				public static bool operator ==
					(
						IVector3D<T> left,
						IVector3D<T> right
					)
					=> left.Equals(right);

				public static bool operator !=
					(
						IVector3D<T> left,
						IVector3D<T> right
					)
					=> left.NotEquals(right);
			}
	}