using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record struct ValueEulerAngle<T>
	{
		public static ValueEulerAngle<T> Zero
			=> new
				(
					Angle<T>.Zero,
					Angle<T>.Zero,
					Angle<T>.Zero
				);
	}