using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using Mikita.Measurement.Angles;
using Mikita.Structs.Scalars;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class EulerAngleStepping
	{
		public static void NormalStepTo<T>
			(
				this Scalar<IEulerAngle<T>> current,
				IEulerAngle<T> target,
				Angle<T> by
			)
			where T:
				struct,
				INumber<T>,
				IRootFunctions<T>
			=> current.Value = current.Value.NormalSteppedTo(target, by);

		public static IEulerAngle<T> NormalSteppedTo<T>
			(
				this IEulerAngle<T> from,
				IEulerAngle<T> to,
				Angle<T> by
			)
			where T:
				struct,
				INumber<T>,
				IRootFunctions<T>
			{
				var local = to - from;
				var length = local.ToRadianVector().Length;
				if (length == T.Zero) return to;
				var normal = local / length;
				return from.SteppedTo(to, normal * by.InRadians);
			}


		public static EulerAngle<T> SteppedTo<T>
			(
				this IEulerAngle<T> from,
				IEulerAngle<T> to,
				IEulerAngle<T> by
			)
			where T:
				INumber<T>,
				IFloatingPointConstants<T>
			{
				return Rotation.Of
					(
						from.X.SteppedTo(to.X, by.X),
						from.Y.SteppedTo(to.Y, by.Y),
						from.Z.SteppedTo(to.Z, by.Z)
					);
			}
	}