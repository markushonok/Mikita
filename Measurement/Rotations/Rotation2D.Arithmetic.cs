using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>
	{
		static Rotation2D<T> operator +
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of
				(
					left.Horizontal + right.Horizontal, 
					left.Vertical + right.Vertical
				);
		
		static Rotation2D<T> operator -
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of
				(
					left.Horizontal - right.Horizontal, 
					left.Vertical - right.Vertical
				);

		static Rotation2D<T> operator *
			(
				Rotation2D<T> left,
				T right
			)
			=> Rotation.Of
				(
					left.Horizontal * right, 
					left.Vertical * right
				);
		
		static Rotation2D<T> operator *
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of
				(
					left.Horizontal * right.Horizontal, 
					left.Vertical * right.Vertical
				);
		
		static Rotation2D<T> operator /
			(
				Rotation2D<T> left, 
				T right
			)
			=> Rotation.Of
				(
					left.Horizontal / right, 
					left.Vertical / right
				);
		
		static Rotation2D<T> operator /
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)
			=> Rotation.Of
				(
					left.Horizontal / right.Horizontal, 
					left.Vertical / right.Vertical
				);
	}