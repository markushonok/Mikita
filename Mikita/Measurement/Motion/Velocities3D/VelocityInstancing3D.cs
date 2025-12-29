using Mikita.Measurement.Motion.Speeds;
using System.Numerics;

namespace Mikita.Measurement.Motion.Velocities3D;

public static class VelocityInstancing3D
	{
		extension<T>(Velocity3D<T>)
			where T: INumber<T>
			{
				public static Velocity3D<T> Zero
					=> new
						(
							Speed<T>.Zero,
							Speed<T>.Zero,
							Speed<T>.Zero
						);
			}
	}