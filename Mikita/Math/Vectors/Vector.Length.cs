using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static T Length<T>(this Vector2D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.LengthTo(vector.X, vector.Y);
		
		public static T Length<T>(this Vector3D<T> vector)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.LengthTo(vector.X, vector.Y, vector.Z);
		
		public static T LengthTo<T>(T x, T y)
			where T : INumber<T>, IRootFunctions<T>
			=> T.Sqrt(x * x + y * y);
		
		public static T LengthTo<T>(T x, T y, T z)
			where T : INumber<T>, IRootFunctions<T>
			=> T.Sqrt(x * x + y * y + z * z);
		
		public static Vector2D<T> LimitLengthTo<T>
			(
				this Vector2D<T> vector,
				T limit
			)
			where T : INumber<T>, IRootFunctions<T>
			=> vector.Normal() * T.Min(vector.Length(), limit);
		
		public static Vector3D<T> LimitLengthTo<T>
			(
				this Vector3D<T> vector,
				T limit
			)
			where T : INumber<T>, IRootFunctions<T>
			=> vector.Normal() * T.Min(vector.Length(), limit);
	}