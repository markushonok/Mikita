using System.Numerics;

namespace Mikita.Measurement.Motion;

public static class VelocityStepping3D
	{
		public static Velocity3D<T> NormalStepTo<T>
			(
				this Velocity3D<T> from,
				Velocity3D<T> to,
				Speed<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var local = to - from;
				var length = local.Speed;
				if (length.Equals(Speed<T>.Zero)) return to;
				var normal = local / length;
				return from.StepTo(to, normal * by);
			}
		
		public static VelocityRecord3D<T> StepTo<T>
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