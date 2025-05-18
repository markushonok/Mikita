namespace Mikita.Math.Vectors;

partial interface Vector2D<out T>
	{
		static Vector2D<T> operator +
			(
				Vector2D<T> augend,
				Vector2D<T> addend
			)
			=> Vector.PointingTo
				(
					augend.X + addend.X,
					augend.Y + addend.Y
				);

		static Vector2D<T> operator -
			(
				Vector2D<T> minuend,
				Vector2D<T> subtrahend
			)
			=> Vector.PointingTo
				(
					minuend.X - subtrahend.X,
					minuend.Y - subtrahend.Y
				);

		static Vector2D<T> operator *
			(
				Vector2D<T> multiplicand,
				T multiplier
			)
			=> multiplicand * Vector.PointingTo(x: multiplier, y: multiplier);

		static Vector2D<T> operator *
			(
				Vector2D<T> multiplicand,
				Vector2D<T> multiplier
			)
			=> Vector.PointingTo
				(
					multiplicand.X * multiplier.X,
					multiplicand.Y * multiplier.Y
				);

		static Vector2D<T> operator /
			(
				Vector2D<T> dividend,
				T divisor
			)
			=> dividend / Vector.PointingTo(x: divisor, y: divisor);

		static Vector2D<T> operator /
			(
				Vector2D<T> dividend,
				Vector2D<T> divisor
			)
			=> Vector.PointingTo
				(
					dividend.X / divisor.X,
					dividend.Y / divisor.Y
				);
	}