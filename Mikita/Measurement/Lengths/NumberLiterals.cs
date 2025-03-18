using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class NumberLiterals
	{
		public static ValueLength<T> km<T>(this T number)
			where T : struct, INumber<T>
			=> new(meters: number * T.CreateChecked(1000));
		
		public static ValueLength<T> m<T>(this T number)
			where T : struct, INumber<T>
			=> new(meters: number);

		public static ValueLength<T> cm<T>(this T number)
			where T : struct, INumber<T>
			=> new(meters: number / T.CreateChecked(100));
		
		public static ValueLength<T> mm<T>(this T number)
			where T : struct, INumber<T>
			=> new(meters: number / T.CreateChecked(1000));
	}