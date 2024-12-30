using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>
	{
		static RotationRecord2D<T> No
			=> new
				(
					T.Zero.rad(), 
					T.Zero.rad()
				);
	}