using Mikita.Measurement.Motion.Speeds;
using Mikita.Measurement.Motion.Velocities2D;
using Mikita.Measurement.Positions;
using System;
using System.Numerics;

namespace Mikita.Measurement.Motion.Velocities3D;

public static class VelocityArithmetic3D
	{
		extension<T>(IVelocity3D<T>)
			where T: INumber<T>, IRootFunctions<T>
			{
				public static Velocity3D<T> operator +
					(
						IVelocity3D<T> augend,
						IVelocity3D<T> addend
					)
					=> new
						(
							augend.X + addend.X,
							augend.Y + addend.Y,
							augend.Z + addend.Z
						);

				public static Velocity3D<T> operator -
					(
						IVelocity3D<T> minuend,
						IVelocity3D<T> subtrahend
					)
					=> new
						(
							minuend.X - subtrahend.X,
							minuend.Y - subtrahend.Y,
							minuend.Z - subtrahend.Z
						);

				public static IVelocity3D<T> operator *
					(
						IVelocity3D<T> multiplicand,
						T multiplier
					)
					=> Velocity.Of
						(
							multiplicand.X * multiplier,
							multiplicand.Y * multiplier,
							multiplicand.Z * multiplier
						);

				public static Position3D<T> operator *
					(
						IVelocity3D<T> velocity,
						TimeSpan duration
					)
					=> Position.At
						(
							velocity.X * duration,
							velocity.Y * duration,
							velocity.Z * duration
						);

				public static IVelocity3D<T> operator /
					(
						IVelocity3D<T> dividend,
						T divisor
					)
					=> Velocity.Of
						(
							dividend.X / divisor,
							dividend.Y / divisor,
							dividend.Z / divisor
						);
			}
	}