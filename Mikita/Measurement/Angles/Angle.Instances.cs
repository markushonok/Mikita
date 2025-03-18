using System.Numerics;

namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static Angle<T> Zero
			=> Angle.Radians(T.Zero);
	}

public static partial class Angle
	{
		public static AngleRecord<T> Radians<T>(T number)

			where T:
				INumber<T>,
				IFloatingPointConstants<T>

			=> new(number);

		public static AngleRecord<T> Degrees<T>(T number)

			where T:
				INumber<T>,
				IFloatingPointConstants<T>

			=> new(InRadians: number * (T.Pi / T.CreateChecked(180)));

		public static AngleRecord<T> Turns<T>(T number)

			where T:
				INumber<T>,
				IFloatingPointConstants<T>

			=> new(InRadians: number * T.CreateChecked(2) * T.Pi);
	}