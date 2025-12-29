using Mikita.Math.Vectors;
using System;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public static class AabbDivision
	{
		public static ValueAabb3D<T> OctantAt<T>
			(
				this IAabb3D<T> aabb,
				int x,
				int y,
				int z
			) where T: struct, INumber<T>, IRootFunctions<T>
			{
				if (x is 0 or 1 && y is 0 or 1 && z is 0 or 1)
					{
						var center = aabb.Center();

						var a = ValueVector.PointingTo
							(
								x == 0 ? aabb.Min.X : center.X,
								y == 0 ? aabb.Min.Y : center.Y,
								z == 0 ? aabb.Min.Z : center.Z
							);

						var b = ValueVector.PointingTo
							(
								x == 0 ? center.X : aabb.Max.X,
								y == 0 ? center.Y : aabb.Max.Y,
								z == 0 ? center.Z : aabb.Max.Z
							);
						return Aabb.With(a, b);
					}
				throw new IndexOutOfRangeException(
					$"Index must be 0 or 1. Received: ({x}; {y}; {z})");
			}
	}