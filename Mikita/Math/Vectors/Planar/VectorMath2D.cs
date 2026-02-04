using Mikita.Math.Numbers;
using Mikita.Math.Sizes;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mikita.Math.Vectors.Planar;

public static class VectorMath2D
	{
		extension(Vector)
			{
				public static Vector2D<T> InMiddleBetween<T>
					(
						IReadOnlyCollection<IVector2D<T>> vectors
					)
					where T: INumber<T>
					=> vectors.Aggregate((left, right) => left + right)
						/ T.CreateChecked(vectors.Count);

				public static Vector2D<T> InMiddleBetween<T>
					(
						Vector2D<T> a,
						Vector2D<T> b
					)
					where T: INumber<T>
					=> (a + b) / T.CreateChecked(2);
			}

		extension<T>(IVector2D<T> vector)
			where T: INumber<T>, IRootFunctions<T>
			{
				public Vector2D<T> LimitLengthTo(T limit)
					=> vector.Normal * T.Min(vector.Length, limit);
			}

		extension<T>(IVector2D<T> vector)
			where T: INumber<T>
			{
				public static Vector2D<T> operator +
					(
						IVector2D<T> augend,
						IVector2D<T> addend
					)
					=> Vector.PointingTo
						(
							augend.X + addend.X,
							augend.Y + addend.Y
						);

				public static Vector2D<T> operator -
					(
						IVector2D<T> minuend,
						IVector2D<T> subtrahend
					)
					=> Vector.PointingTo
						(
							minuend.X - subtrahend.X,
							minuend.Y - subtrahend.Y
						);

				public static Vector2D<T> operator *
					(
						IVector2D<T> multiplicand,
						T multiplier
					)
					=> multiplicand * Vector.PointingTo(x: multiplier, y: multiplier);

				public static Vector2D<T> operator *
					(
						IVector2D<T> multiplicand,
						IVector2D<T> multiplier
					)
					=> Vector.PointingTo
						(
							multiplicand.X * multiplier.X,
							multiplicand.Y * multiplier.Y
						);

				public static Vector2D<T> operator /
					(
						IVector2D<T> dividend,
						T divisor
					)
					=> dividend / Vector.PointingTo(x: divisor, y: divisor);

				public static Vector2D<T> operator /
					(
						IVector2D<T> dividend,
						ISize2D<T> divisor
					)
					=> dividend / divisor.AsVector;

				public static Vector2D<T> operator /
					(
						IVector2D<T> dividend,
						IVector2D<T> divisor
					)
					=> Vector.PointingTo
						(
							dividend.X / divisor.X,
							dividend.Y / divisor.Y
						);
			}
	}