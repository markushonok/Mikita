using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Math.Vectors.Spatial;

public static class VectorMath3D
	{
		extension<T>(IVector3D<T> vector)
			where T: INumber<T>, IRootFunctions<T>
			{
				public IVector3D<T> LimitLengthTo(T limit)
					=> vector.Normal * T.Min(vector.Length, limit);

				public IVector3D<T> Normal
					=> vector.IsZero
						? vector
						: vector.UnsafeNormal;

				public Vector3D<T> UnsafeNormal
					=> vector / vector.Length;

				public T Length
					=> T.Sqrt
						(
							vector.X * vector.X
								+ vector.Y * vector.Y
								+ vector.Z * vector.Z
						);
			}

		extension<T>(IVector3D<T> vector)
			where T: INumber<T>
			{
				public Vector3D<T> Sign
					=> Vector.PointingTo
						(
							vector.X.TSign,
							vector.Y.TSign,
							vector.Z.TSign
						);
			}

		extension<T>(IVector3D<T>)
			where T: INumber<T>
			{
				public static Vector3D<T> operator +
					(
						IVector3D<T> augend,
						IVector3D<T> addend
					)
					=> Vector.PointingTo
						(
							augend.X + addend.X,
							augend.Y + addend.Y,
							augend.Z + addend.Z
						);

				public static Vector3D<T> operator -
					(
						IVector3D<T> minuend,
						IVector3D<T> subtrahend
					)
					=> Vector.PointingTo
						(
							minuend.X - subtrahend.X,
							minuend.Y - subtrahend.Y,
							minuend.Z - subtrahend.Z
						);

				public static Vector3D<T> operator *
					(
						IVector3D<T> multiplicand,
						T multiplier
					)
					=> multiplicand * Vector.PointingTo
						(
							x: multiplier,
							y: multiplier,
							z: multiplier
						);

				public static Vector3D<T> operator *
					(
						IVector3D<T> multiplicand,
						IVector3D<T> multiplier
					)
					=> Vector.PointingTo
						(
							multiplicand.X * multiplier.X,
							multiplicand.Y * multiplier.Y,
							multiplicand.Z * multiplier.Z
						);

				public static Vector3D<T> operator /
					(
						IVector3D<T> dividend,
						T divisor
					)
					=> dividend / Vector.PointingTo
						(
							x: divisor,
							y: divisor,
							z: divisor
						);

				public static Vector3D<T> operator /
					(
						IVector3D<T> dividend,
						IVector3D<T> divisor
					)
					=> Vector.PointingTo
						(
							dividend.X / divisor.X,
							dividend.Y / divisor.Y,
							dividend.Z / divisor.Z
						);
			}
	}