using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record EulerAngle<T>
	{
		public static EulerAngle<T> Zero
			=> new
				(
					Angle<T>.Zero,
					Angle<T>.Zero,
					Angle<T>.Zero
				);
	}