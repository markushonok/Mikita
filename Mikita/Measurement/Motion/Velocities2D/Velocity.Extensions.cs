using Mikita.Measurement.Motion.Speeds;
using Mikita.Measurement.Motion.Velocities3D;
using System.Numerics;
using SpeedClass = Mikita.Measurement.Motion.Speeds.Speed;

namespace Mikita.Measurement.Motion.Velocities2D;

public static partial class Velocity
	{
		extension<T>
			(
				IVelocity3D<T> velocity
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				public Speed<T> Speed
					=> SpeedClass.OfMetersPerSecond
						(
							T.Sqrt
								(
									velocity.X.MetersPerSecond * velocity.X.MetersPerSecond
									+ velocity.Y.MetersPerSecond * velocity.Y.MetersPerSecond
									+ velocity.Z.MetersPerSecond * velocity.Z.MetersPerSecond
								)
						);
			}
	}