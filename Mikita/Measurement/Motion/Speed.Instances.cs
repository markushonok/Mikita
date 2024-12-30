using System.Numerics;

namespace Mikita.Measurement.Motion;

public partial interface Speed<out T>
	{
		public static SpeedRecord<T> Zero
			=> Speed.InMetersPerSecond(T.Zero);
	}

public static partial class Speed
	{
		public static SpeedRecord<T> InMetersPerSecond<T>(T number)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> new(InMetersPerSecond: number);
	}