using Mikita.Structs.Scalars;
using System.Numerics;

namespace Mikita.Measurement.Motion;

public static class VelocityStepping3D
	{
		public static void NormalStepTo<T>
			(
				this Scalar<Velocity3D<T>> scalar,
				Velocity3D<T> to,
				Speed<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			=> scalar.Value = scalar.Value.NormalSteppedTo(to, by);

		public static Velocity3D<T> NormalSteppedTo<T>
			(
				this Velocity3D<T> from,
				Velocity3D<T> to,
				Speed<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var local = to - from;
				var length = local.Speed();
				if (length.Equals(Speed<T>.Zero)) return to;
				var normal = local / length.InMetersPerSecond;
				return from.SteppedTo(to, normal * by.InMetersPerSecond);
			}
		
		public static VelocityRecord3D<T> SteppedTo<T>
			(
				this Velocity3D<T> from,
				Velocity3D<T> to,
				Velocity3D<T> by
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