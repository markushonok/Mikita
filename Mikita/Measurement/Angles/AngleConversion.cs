using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleConversion
	{
		extension<T>(IAngle<T> angle)
			where T:
				INumber<T>,
				IFloatingPointConstants<T>
			{
				T InDegrees
					=> angle.InRadians * (T.CreateChecked(180) / T.Pi);
			}
	}