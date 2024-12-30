using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Measurement.Motion;

public static class SpeedStepping
	{
		public static SpeedRecord<T> StepTo<T>
			(
				this Speed<T> from,
				Speed<T> to,
				Speed<T> by
			)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Speed.InMetersPerSecond
				(
					from.InMetersPerSecond.StepTo
						(
							to.InMetersPerSecond, 
							by.InMetersPerSecond
						)
				);
	}