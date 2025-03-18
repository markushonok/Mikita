using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static Vector2D<T> Normal<T>(this Vector2D<T> vector)
			where T: INumber<T>, IRootFunctions<T>
			=> vector / vector.Length();
		
		public static Vector3D<T> Normal<T>(this Vector3D<T> vector)
			where T: INumber<T>, IRootFunctions<T>
			=> vector / vector.Length();
	}