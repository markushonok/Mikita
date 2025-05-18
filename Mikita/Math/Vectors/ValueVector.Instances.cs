using System.Numerics;

namespace Mikita.Math.Vectors;

public static class ValueVector
	{
		public static ValueVector3D<T> To<T>
			(
				T x,
				T y,
				T z
			)
			where T: struct, INumber<T>, IRootFunctions<T>
			=> new(x, y, z);
	}