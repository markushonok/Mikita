using System.Numerics;

namespace Mikita.Math.Vectors;

public static class Vector3D
	{
		public static Vector3D<T> Of<T>(T number)
			where T: INumber<T>, IRootFunctions<T>
			=> Vector.PointingTo(number, number, number);
	}