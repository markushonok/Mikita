using System.Numerics;
using SpeedClass = Mikita.Measurement.Motion.Speed;

namespace Mikita.Measurement.Motion;

public static partial class Velocity
	{
		internal static Speed<T> Speed<T>
			(
				this Velocity3D<T> velocity
			)
		
			where T: INumber<T>, IRootFunctions<T>
		
			=> SpeedClass.Sqrt
				(
					velocity.X * velocity.X
					+ velocity.Y * velocity.Y
					+ velocity.Z * velocity.Z
				);
	}