using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleInstancing
	{
		extension<T>(Angle<T>)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				public static Angle<T> Zero
					=> AngleInstancing<T>.Zero;
			}

		extension<T>(Angle)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				public static Angle<T> Radians(T number)
					=> new(number);

				public static Angle<T> Degrees(T number)
					=> new(InRadians: number * (T.Pi / T.CreateChecked(180)));

				public static Angle<T> Turns(T number)
					=> new(InRadians: number * T.CreateChecked(2) * T.Pi);
			}
	}

file static class AngleInstancing<T>
	where T: INumber<T>, IFloatingPointConstants<T>
	{
		public static Angle<T> Zero { get; }
			= Angle.Radians(T.Zero);
	}