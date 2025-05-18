using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class EulerAngleComparison
	{
		public static bool IsNotZero<T>
			(
				this IEulerAngle<T> eulerAngle
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			=> !eulerAngle.IsZero();

		public static bool IsZero<T>
			(
				this IEulerAngle<T> eulerAngle
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				return eulerAngle.X.Equals(Angle<T>.Zero)
					&& eulerAngle.Y.Equals(Angle<T>.Zero)
					&& eulerAngle.Z.Equals(Angle<T>.Zero);
			}

	}