using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class EulerAngleComparison
	{
		extension<T>(IEulerAngle<T> eulerAngle)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				public bool IsNotZero
					=> !eulerAngle.IsZero;

				public bool IsZero
					=> eulerAngle.X.Equals(Angle<T>.Zero)
						&& eulerAngle.Y.Equals(Angle<T>.Zero)
						&& eulerAngle.Z.Equals(Angle<T>.Zero);
			}

	}