using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Measurement.Motion.Speeds;

public static class SpeedStepping
	{
		extension<T>(ISpeed<T> from)
			where T: INumber<T>, IRootFunctions<T>
			{
				public Speed<T> StepTo
					(
						ISpeed<T> to,
						ISpeed<T> by
					)
					=> Speed.OfMetersPerSecond
						(
							from.MetersPerSecond.Stepped
								(
									to.MetersPerSecond,
									by.MetersPerSecond
								)
						);
			}
	}