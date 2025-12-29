using System.Numerics;

namespace Mikita.Measurement.Motion.Speeds;

public static class SpeedInstancing
	{
		extension<T>(Speed<T>)
			where T: INumber<T>
			{
				public static Speed<T> Zero
					=> Speed.OfMetersPerSecond(T.Zero);
			}

		extension(Speed)
			{
				public static Speed<T> OfMetersPerSecond<T>
					(
						T number
					)
					where T: INumber<T>
					=> new
						(
							MetersPerSecond: number
						);
			}
	}