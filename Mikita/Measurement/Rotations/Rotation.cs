using Mikita.Measurement.Angles;
using Mikita.Measurement.Rotations.EulerAngles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public static class Rotation
	{
		public static EulerAngle<T> Of<T>
			(
				Angle<T> x,
				Angle<T> y,
				Angle<T> z,
				EulerOrder order
			)
		
			where T 
				: INumber<T>
				, IFloatingPointConstants<T>
		
			=> new(x, y, z, order);
		
		public static EulerAngle<T> Min<T>
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)

			where T 
				: INumber<T>
				, IFloatingPointConstants<T>
		
			=> new
				(
					Angle.Min(left.X, right.X),
					Angle.Min(left.Y, right.Y),
					Angle.Min(left.Z, right.Z),
					left.Order
				);
		
		public static EulerAngle<T> Max<T>
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)

			where T 
			: INumber<T>
			, IFloatingPointConstants<T>
		
			=> new
				(
					Angle.Max(left.X, right.X),
					Angle.Max(left.Y, right.Y),
					Angle.Max(left.Z, right.Z),
					left.Order
				);
	}
