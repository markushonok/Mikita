using Mikita.Math.Points;
using Mikita.Math.Vectors.Planar;
using Mikita.Math.Vectors.Spatial;
using Mikita.Measurement.Positions;
using Mikita.Measurement.Sizes;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class VectorInstancing
	{
		extension(Vector)
			{
				public static Vector3D<T> PointingTo<T>
					(
						IPosition3D<T> position
					)
					where T: INumber<T>, IRootFunctions<T>
					=> Vector.PointingTo
						(
							position.X.Meters,
							position.Y.Meters,
							position.Z.Meters
						);

				public static Vector3D<T> WithAxes<T>
					(
						ISize3D<T> size
					)
					where T: INumber<T>, IRootFunctions<T>
					=> Vector.PointingTo
						(
							x: size.Width.Meters,
							y: size.Height.Meters,
							z: size.Depth.Meters
						);

				public static Vector2D<T> To<T>
					(
						IPoint2D<T> point
					)
					=> new(point.X, point.Y);

				public static Vector2D<T> PointingTo<T>
					(
						T x,
						T y
					)
					=> new(x, y);

				public static Vector3D<T> PointingTo<T>
					(
						T x,
						T y,
						T z
					)
					=> new(x, y, z);
			}
	}