using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class ValueEulerAngleInstancing
	{
		extension<T>(ValueEulerAngle<T>)
			where T:
				struct,
				INumber<T>,
				IFloatingPointConstants<T>
			{
				public static ValueEulerAngle<T> Zero
					=> ValueEulerAngleInstancing<T>.Zero;
			}
	}

file static class ValueEulerAngleInstancing<T>
	where T:
		struct,
		INumber<T>,
		IFloatingPointConstants<T>
	{
		public static ValueEulerAngle<T> Zero
			=> new
				(
					Angle<T>.Zero,
					Angle<T>.Zero,
					Angle<T>.Zero
				);
	}