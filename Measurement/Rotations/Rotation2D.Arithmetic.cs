namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>
	{
		static Rotation2D<T> operator +
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of(left.X + right.X, left.Y + right.Y);
		
		static Rotation2D<T> operator -
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of(left.X - right.X, left.Y - right.Y);
		
		static Rotation2D<T> operator *
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of(left.X * right.X, left.Y * right.Y);
		
		static Rotation2D<T> operator /
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of(left.X / right.X, left.Y / right.Y);
	}