using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static VectorRecord2D<T> PointingTo<T>(T x, T y)
			where T : INumber<T>, IRootFunctions<T>
			=> new(x, y);

		public static VectorRecord3D<T> PointingTo<T>(T x, T y, T z)
			where T : INumber<T>, IRootFunctions<T>
			=> new(x, y, z);
	}