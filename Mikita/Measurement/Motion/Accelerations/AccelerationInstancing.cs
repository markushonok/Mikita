namespace Mikita.Measurement.Motion.Accelerations;

public static class AccelerationInstancing
	{
		extension<T>(Acceleration)
			{
				public static Acceleration<T> OfMetersPerSecond2
					(
						T metersPerSecond2
					)
					=> new(metersPerSecond2);
			}
	}