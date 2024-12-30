using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		internal static VectorRecord2D<T> Sign<T>(this Vector2D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.SignOf(vector.X, vector.Y);
		
		internal static VectorRecord3D<T> Sign<T>(this Vector3D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.SignOf(vector.X, vector.Y, vector.Z);
		
		public static VectorRecord2D<T> SignOf<T>(T x, T y)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.PointingTo(x.Sign(), y.Sign());
		
		public static VectorRecord3D<T> SignOf<T>(T x, T y, T z)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.PointingTo(x.Sign(), y.Sign(), z.Sign());
	}