using System.Numerics;

namespace Mikita.Measurement.Lengths;

public sealed partial class Length<T>
	{
		public static Length<T> Zero { get; }
			= new(T.Zero);
	}

public static class Length
	{
		public static ValueLength<float> FromMeters(float number)
			=> new(meters: number);

		public static Length<T> FromMeters<T>(T number)
			where T: INumber<T>
			=> new(meters: number);

		public static Length<T> FromCentimeters<T>(T number)
			where T: INumber<T>
			=> new(meters: number * T.CreateChecked(0.01));

		public static Length<T> FromInches<T>(T number)
			where T: INumber<T>
			=> new(meters: number * T.CreateChecked(2.54));
	}