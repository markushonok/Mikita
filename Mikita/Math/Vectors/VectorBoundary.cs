using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class VectorBoundary
	{
		public static bool IsOutside<T>
			(
				this IVector3D<T> target,
				IVector3D<T> min,
				IVector3D<T> max
			)
			where T: INumber<T>, IRootFunctions<T>
			=> !target.IsBetween(min, max);

		public static bool IsBetween<T>
			(
				this IVector3D<T> target,
				IVector3D<T> min,
				IVector3D<T> max
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				return
					target.X >= min.X && target.X <= max.X
					&& target.Y >= min.Y && target.Y <= max.Y
					&& target.Z >= min.Z && target.Z <= max.Z;
			}
	}