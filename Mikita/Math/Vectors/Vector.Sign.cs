using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static VectorRecord2D<T> Sign<T>(this Vector2D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> SignOf(vector.X, vector.Y);
		
		public static Vector3D<T> Sign<T>(this IVector3D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> SignOf(vector.X, vector.Y, vector.Z);

		public static VectorRecord2D<T> SignOf<T>(T x, T y)
			where T : INumber<T>, IRootFunctions<T>
			=> PointingTo(x.Sign(), y.Sign());

		public static Vector3D<T> SignOf<T>(T x, T y, T z)
			where T : INumber<T>, IRootFunctions<T>
			=> PointingTo(x.Sign(), y.Sign(), z.Sign());
	}