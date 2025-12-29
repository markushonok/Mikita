using Mikita.Measurement.Motion.Speeds;
using Mikita.Measurement.Motion.Velocities3D;

namespace Mikita.Measurement.Motion.Velocities2D;

public static partial class Velocity
	{
		public static Velocity3D<T> Of<T>
			(
				ISpeed<T> x,
				ISpeed<T> y,
				ISpeed<T> z
			)
			=> new(x, y, z);
	}