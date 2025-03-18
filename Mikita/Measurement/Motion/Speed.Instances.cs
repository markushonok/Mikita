using System.Numerics;

namespace Mikita.Measurement.Motion;

partial interface Speed<out T>
	{
		public static SpeedRecord<T> Zero
			=> Speed.FromMetersPerSecond(T.Zero);
	}

public static class Speed
	{
		public static SpeedRecord<T> FromMetersPerSecond<T>(T number)
			where T: INumber<T>, IRootFunctions<T>
			=> new(InMetersPerSecond: number);
	}