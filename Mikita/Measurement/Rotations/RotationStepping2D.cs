using Mikita.Many.Scalars;
using Mikita.Measurement.Angles;
using Mikita.Structs.Scalars;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public static class RotationStepping2D
	{
		public static void NormalStepTo<T>
			(
				this Scalar<Rotation2D<T>> current,
				Rotation2D<T> target,
				Angle<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			=> current.Value = current.Value.NormalSteppedTo(target, by);

		public static Rotation2D<T> NormalSteppedTo<T>
			(
				this Rotation2D<T> from,
				Rotation2D<T> to,
				Angle<T> by
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var local = to - from;
				var length = local.ToRadianVector().Length;
				if (length == T.Zero) return to;
				var normal = local / length;
				return from.SteppedTo(to, normal * by.InRadians);
			}


		public static RotationRecord2D<T> SteppedTo<T>
			(
				this Rotation2D<T> from,
				Rotation2D<T> to,
				Rotation2D<T> by
			)
			where T: 
				INumber<T>, 
				IFloatingPointConstants<T>
			{
				return Rotation.Of
					(
						from.Horizontal.SteppedTo(to.Horizontal, by.Horizontal),
						from.Vertical.SteppedTo(to.Vertical, by.Vertical)
					);
			}
	}