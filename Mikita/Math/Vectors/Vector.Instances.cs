using Mikita.Measurement.Positions;
using Mikita.Measurement.Sizes;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static Vector3D<T> PointingTo<T>(IPosition3D<T> position)
			where T: INumber<T>, IRootFunctions<T>
			=> PointingTo
				(
					position.X.Meters(),
					position.Y.Meters(),
					position.Z.Meters()
				);

		public static Vector3D<T> WithAxes<T>(ISize3D<T> size)
			where T: INumber<T>, IRootFunctions<T>
			=> PointingTo
				(
					x: size.Width.Meters(),
					y: size.Height.Meters(),
					z: size.Depth.Meters()
				);

		public static VectorRecord2D<T> PointingTo<T>(T x, T y)
			where T : INumber<T>, IRootFunctions<T>
			=> new(x, y);

		public static Vector3D<T> PointingTo<T>(T x, T y, T z)
			where T : INumber<T>, IRootFunctions<T>
			=> new(x, y, z);
	}