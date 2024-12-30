using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public static class Rotation
	{
		public static RotationRecord2D<T> Of<T>
			(
				Angle<T> horizontal, 
				Angle<T> vertical
			)
		
			where T 
				: INumber<T>
				, IFloatingPointConstants<T>
		
			=> new(horizontal, vertical);
		
		public static RotationRecord2D<T> Min<T>
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)

			where T 
				: INumber<T>
				, IFloatingPointConstants<T>
		
			=> new
				(
					Angle.Min(left.Horizontal, right.Horizontal),
					Angle.Min(left.Vertical, right.Vertical)
				);
		
		public static RotationRecord2D<T> Max<T>
			(
				Rotation2D<T> left, 
				Rotation2D<T> right
			)

			where T 
			: INumber<T>
			, IFloatingPointConstants<T>
		
			=> new
				(
					Angle.Max(left.Horizontal, right.Horizontal),
					Angle.Max(left.Vertical, right.Vertical)
				);
	}