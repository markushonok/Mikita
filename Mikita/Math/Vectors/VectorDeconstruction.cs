using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class VectorDeconstruction
	{
		public static void Deconstruct<T>
			(
				this IVector3D<T> vector,
				out T x,
				out T y,
				out T z
			)
			where T : INumber<T>, IRootFunctions<T>
			{
				x = vector.X;
				y = vector.Y;
				z = vector.Z;
			}
	}