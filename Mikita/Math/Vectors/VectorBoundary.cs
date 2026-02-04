using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class VectorBoundary
	{
		extension<T>(IVector3D<T> target)
			where T: INumber<T>, IRootFunctions<T>
			{
				public bool IsOutside
					(
						IVector3D<T> min,
						IVector3D<T> max
					)
					=> !target.IsBetween(min, max);

				public bool IsBetween
					(
						IVector3D<T> min,
						IVector3D<T> max
					)
					=> target.X >= min.X && target.X <= max.X
						&& target.Y >= min.Y && target.Y <= max.Y
						&& target.Z >= min.Z && target.Z <= max.Z;
			}

	}