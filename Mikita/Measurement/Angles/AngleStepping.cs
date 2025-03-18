using Mikita.Math.Numbers;
using Mikita.Structs.Scalars;
using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleStepping
	{
		public static void StepTo<T>
			(
				this Scalar<Angle<T>> from,
				Angle<T> to,
				Angle<T> by
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			=> from.Value = from.Value.SteppedTo(to, by);

		public static Angle<T> SteppedTo<T>
			(
				this Angle<T> from,
				Angle<T> to,
				Angle<T> by
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				var number = from.InRadians.Stepped(
					to.InRadians,
					by.InRadians);

				return Angle.Radians(number);
			}
	}