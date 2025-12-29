using Mikita.Measurement.Motion.Speeds;
using System;
using System.Numerics;

namespace Mikita.Measurement.Motion.Accelerations;

public static class AccelerationArithmetic
	{
		extension<T>(IAcceleration<T>)
			where T: INumber<T>
			{
				public static Speed<T> operator *
					(
						IAcceleration<T> multiplicand,
						TimeSpan multiplier
					)
					=> Speed.OfMetersPerSecond
						(
							multiplicand.MetersPerSecond2
								* T.CreateChecked(multiplier.TotalSeconds)
						);

				public static Acceleration<T> operator *
					(
						IAcceleration<T> multiplicand,
						T multiplier
					)
					=> Acceleration.OfMetersPerSecond2
						(
							multiplicand.MetersPerSecond2 * multiplier
						);
			}
	}