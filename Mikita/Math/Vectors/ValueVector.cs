using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class ValueVector
	{
		public static ValueVector3D<T> PointingTo<T>
			(
				T x,
				T y,
				T z
			)
			where T: struct
			=> new(x, y, z);
	}