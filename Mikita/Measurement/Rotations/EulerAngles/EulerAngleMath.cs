using System.Numerics;
using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class EulerAngleMath
	{
		extension<T>(IEulerAngle<T>)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				public static IEulerAngle<T> operator +
					(
						IEulerAngle<T> augend,
						IEulerAngle<T> addend
					)
					=> Rotation.Of
						(
							augend.X + addend.X,
							augend.Y + addend.Y,
							augend.Z + addend.Z
						);

				public static IEulerAngle<T> operator -
					(
						IEulerAngle<T> minuend,
						IEulerAngle<T> subtrahend
					)
					=> Rotation.Of
						(
							minuend.X - subtrahend.X,
							minuend.Y - subtrahend.Y,
							minuend.Z - subtrahend.Z
						);

				public static IEulerAngle<T> operator *
					(
						IEulerAngle<T> multiplicand,
						T multiplier
					)
					=> Rotation.Of
						(
							multiplicand.X * multiplier,
							multiplicand.Y * multiplier,
							multiplicand.Z * multiplier
						);

				public static IEulerAngle<T> operator *
					(
						IEulerAngle<T> multiplicand,
						IEulerAngle<T> multiplier
					)
					=> Rotation.Of
						(
							multiplicand.X * multiplier.X,
							multiplicand.Y * multiplier.Y,
							multiplicand.Z * multiplier.Z
						);

				public static IEulerAngle<T> operator /
					(
						IEulerAngle<T> dividend,
						T divisor
					)
					=> Rotation.Of
						(
							dividend.X / divisor,
							dividend.Y / divisor,
							dividend.Z / divisor
						);

				public static IEulerAngle<T> operator /
					(
						IEulerAngle<T> dividend,
						IEulerAngle<T> divisor
					)
					=> Rotation.Of
						(
							dividend.X / divisor.X,
							dividend.Y / divisor.Y,
							dividend.Z / divisor.Z
						);
			}
	}