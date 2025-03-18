using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

/// <summary>
/// Provides methods for converting Euler angles between different rotation orders.
/// </summary>
public static class EulerAngleConversion
	{
		public static IEulerAngle<T> To<T>
			(
				this IEulerAngle<T> eulerAngle,
				EulerOrder order
			)
			where T:
				INumber<T>,
				IFloatingPointConstants<T>
			{

			}
	}