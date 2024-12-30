namespace Mikita.Math.Vectors;

public partial interface Vector2D<out T>
	{
		static Vector2D<T> operator +
			(
				Vector2D<T> left,
				Vector2D<T> right
			)
			=> Vector.PointingTo(left.X + right.X, left.Y + right.Y);
		
		static Vector2D<T> operator -
			(
				Vector2D<T> left,
				Vector2D<T> right
			)
			=> Vector.PointingTo(left.X - right.X, left.Y - right.Y);
		
		static Vector2D<T> operator *
			(
				Vector2D<T> vector,
				T scalar
			)
			=> vector * Vector.PointingTo(scalar, scalar);
		
		static Vector2D<T> operator *
			(
				Vector2D<T> left,
				Vector2D<T> right
			)
			=> Vector.PointingTo(left.X * right.X, left.Y * right.Y);
		
		static Vector2D<T> operator /
			(
				Vector2D<T> vector,
				T scalar
			)
			=> vector / Vector.PointingTo(scalar, scalar);
		
		static Vector2D<T> operator /
			(
				Vector2D<T> left,
				Vector2D<T> right
			)
			=> Vector.PointingTo(left.X / right.X, left.Y / right.Y);
	}