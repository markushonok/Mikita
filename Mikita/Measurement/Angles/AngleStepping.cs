using Mikita.Math.Numbers;
using Mikita.Structs.Referring;
using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleStepping
	{
		public static void StepTo<T>
			(
				this IRef<IAngle<T>> from,
				IAngle<T> to,
				IAngle<T> by
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			=> from.Value = from.Value.SteppedTo(to, by);

		public static IAngle<T> SteppedTo<T>
			(
				this IAngle<T> from,
				IAngle<T> to,
				IAngle<T> by
			)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				var number = from.InRadians.Stepped(
					to.InRadians,
					by.InRadians);

				return Angle.Radians(number);
			}
	}