using Mikita.Measurement.Motion.Speeds;
using Mikita.Measurement.Motion.Velocities2D;
using Mikita.Structs.Scalars;
using System.Numerics;

namespace Mikita.Measurement.Motion.Velocities3D;

public static class VelocityStepping3D
	{
		public static void NormalStep<T>
			(
				this Scalar<IVelocity3D<T>> scalar,
				IVelocity3D<T> to,
				Speed<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			=> scalar.Value = scalar.Value.NormalSteppedTo(to, by);

		public static IVelocity3D<T> NormalSteppedTo<T>
			(
				this IVelocity3D<T> from,
				IVelocity3D<T> to,
				Speed<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var local = to - from;
				var length = local.Speed;
				if (length.Equals(Speed<T>.Zero)) return to;
				var normal = local / length.MetersPerSecond;
				return from.SteppedTo(to, normal * by.MetersPerSecond);
			}
		
		public static Velocity3D<T> SteppedTo<T>
			(
				this IVelocity3D<T> from,
				IVelocity3D<T> to,
				IVelocity3D<T> by
			)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Velocity.Of
				(
					x: from.X.StepTo(to.X, by.X),
					y: from.Y.StepTo(to.Y, by.Y),
					z: from.Z.StepTo(to.Z, by.Z)
				);
	}