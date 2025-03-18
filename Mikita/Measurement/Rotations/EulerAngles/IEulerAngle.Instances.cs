using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial interface IEulerAngle<out T>
	{
		static EulerAngle<T> No
			=> new
				(
					T.Zero.rad(), 
					T.Zero.rad()
				);
	}