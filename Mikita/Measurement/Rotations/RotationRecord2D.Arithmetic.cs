namespace Mikita.Measurement.Rotations;

public partial record struct RotationRecord2D<T>
	{
		public static Rotation2D<T> operator +
			(
				RotationRecord2D<T> left, 
				Rotation2D<T> right
			)
			=> (Rotation2D<T>) left + right;
		
		public static Rotation2D<T> operator -
			(
				RotationRecord2D<T> left, 
				Rotation2D<T> right
			)
			=> (Rotation2D<T>) left - right;

		public static Rotation2D<T> operator *
			(
				RotationRecord2D<T> left,
				T right
			)
			=> (Rotation2D<T>) left * right;
		
		public static Rotation2D<T> operator *
			(
				RotationRecord2D<T> left, 
				Rotation2D<T> right
			)
			=> (Rotation2D<T>) left * right;
		
		public static Rotation2D<T> operator /
			(
				RotationRecord2D<T> left, 
				T right
			)
			=> (Rotation2D<T>) left / right;
		
		public static Rotation2D<T> operator /
			(
				RotationRecord2D<T> left, 
				Rotation2D<T> right
			)
			=> (Rotation2D<T>) left / right;
	}