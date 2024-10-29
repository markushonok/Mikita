using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public static class Rotation
	{
		public static RotationRecord2D<T> Of<T>
			(
				Angle<T> x, 
				Angle<T> y
			)
		
			where T 
				: INumber<T>
				, IFloatingPointConstants<T>
		
			=> new(x, y);

		public static Rotation2D Of
			(
				Angle<float> x,
				Angle<float> y
			)
			{
				return Rotation2D.Of(
					Rotation.Of<float>(x, y));
			}
	}