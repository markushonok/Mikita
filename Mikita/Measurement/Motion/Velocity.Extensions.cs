using System.Numerics;
using SpeedClass = Mikita.Measurement.Motion.Speed;

namespace Mikita.Measurement.Motion;

public static partial class Velocity
	{
		public static Speed<T> Speed<T>
			(
				this Velocity3D<T> velocity
			)
			where T : INumber<T>, IRootFunctions<T>
			{
				var sqrt = T.Sqrt
					(
						velocity.X.InMetersPerSecond * velocity.X.InMetersPerSecond
						+ velocity.Y.InMetersPerSecond * velocity.Y.InMetersPerSecond
						+ velocity.Z.InMetersPerSecond * velocity.Z.InMetersPerSecond
					);
				return SpeedClass.FromMetersPerSecond(sqrt);
			}
	}