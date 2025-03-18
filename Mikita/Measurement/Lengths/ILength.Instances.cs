using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class ZeroValueLength<T>
	where T : struct, INumber<T>
	{
		public static ValueLength<T> Value
			=> new(T.Zero);
	}

public static class ZeroLength<T>
	where T : INumber<T>
	{
		public static Length<T> Reference { get; }
			= new(T.Zero);
	}

public static class Length
	{
		public static ValueLength<float> FromMeters(float number)
			=> new(number);

		public static ValueLength<T> FromValueMeters<T>(T number)
			where T : struct, INumber<T>
			=> new(number);

		public static Length<T> FromMeters<T>(T number)
			where T : INumber<T>
			=> new(number);
	}