using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public static class AabbPoint
	{
		public static IVector3D<T> Center<T>
			(
				this IAabb3D<T> aabb
			)
			where T : INumber<T>, IRootFunctions<T>
			=> (aabb.Min + aabb.Max) * T.CreateChecked(0.5);
	}