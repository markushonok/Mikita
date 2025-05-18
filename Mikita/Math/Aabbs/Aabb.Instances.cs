using Mikita.Math.Vectors;
using Mikita.Measurement.Sizes;
using System.Numerics;
using Vector = Mikita.Math.Vectors.Vector;

namespace Mikita.Math.Aabbs;

public static class Aabb
	{
		public static Aabb3D<T> With<T>(ISize3D<T> size)
			where T: INumber<T>, IRootFunctions<T>
			=> Aabb.WithSize(Vector.WithAxes(size));

		public static Aabb3D<T> WithSize<T>(IVector3D<T> size)
			where T: INumber<T>, IRootFunctions<T>
			=> new
				(
					Min: size * -T.CreateChecked(0.5),
					Max: size * T.CreateChecked(0.5)
				);

		public static Aabb3D<T> With<T>
			(
				IVector3D<T> min,
				IVector3D<T> max
			)
			where T: INumber<T>, IRootFunctions<T>
			=> new(min, max);

		public static ValueAabb3D<T> With<T>
			(
				ValueVector3D<T> min,
				ValueVector3D<T> max
			)
			where T: struct, INumber<T>, IRootFunctions<T>
			=> new(min, max);
	}