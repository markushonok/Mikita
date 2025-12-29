using Mikita.Objects;
using System.Numerics;

namespace Mikita.Measurement.Motion.Velocities3D;

public static class VelocityComparison3D
	{
		extension<T>(IVelocity3D<T> velocity)
			where T: INumber<T>
			{
				public bool IsNotZero
					=> velocity != Velocity3D<T>.Zero;

				public bool IsZero
					=> velocity == Velocity3D<T>.Zero;
			}

		extension<T>(IVelocity3D<T>)
			{
				public static bool operator ==
					(
						IVelocity3D<T> left,
						IVelocity3D<T> right
					)
					=> left.Equals(right);

				public static bool operator !=
					(
						IVelocity3D<T> left,
						IVelocity3D<T> right
					)
					=> left.NotEquals(right);
			}
	}