using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleMath
	{
		extension<T>(Angle)
			where T:
				INumber<T>,
				IFloatingPointConstants<T>
			{
				public static IAngle<T> Min
					(
						IAngle<T> left,
						IAngle<T> right
					)
					=> left < right ? left : right;

				public static IAngle<T> Max
					(
						IAngle<T> left,
						IAngle<T> right
					)
					=> left < right ? left : right;
			}

		extension<T>(IAngle<T>)
			where T:
				INumber<T>,
				IFloatingPointConstants<T>
			{
				public static Angle<T> operator +
					(
						IAngle<T> augend,
						IAngle<T> addend
					)
					=> Angle.Radians(augend.InRadians + addend.InRadians);

				public static Angle<T> operator -
					(
						IAngle<T> minuend,
						IAngle<T> subtrahend
					)
					=> Angle.Radians(minuend.InRadians - subtrahend.InRadians);

				public static Angle<T> operator *
					(
						IAngle<T> multiplicand,
						T multiplier
					)
					=> Angle.Radians(multiplicand.InRadians * multiplier);

				public static Angle<T> operator *
					(
						IAngle<T> multiplicand,
						IAngle<T> multiplier
					)
					=> Angle.Radians(multiplicand.InRadians * multiplier.InRadians);

				public static Angle<T> operator /
					(
						IAngle<T> dividend,
						T divisor
					)
					=> Angle.Radians(dividend.InRadians / divisor);

				public static Angle<T> operator /
					(
						IAngle<T> dividend,
						IAngle<T> divisor
					)
					=> Angle.Radians(dividend.InRadians / divisor.InRadians);
			}
	}