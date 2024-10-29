using System.Numerics;

namespace Mikita.Math.Points;

public static class Point
	{
		public static PointRecord2D<T> At<T>(T x, T y)
			where T : INumber<T>
			=> new(x, y);
		
		public static PointRecord3D<T> At<T>(T x, T y, T z)
			where T : INumber<T>
			=> new(x, y, z);
	}