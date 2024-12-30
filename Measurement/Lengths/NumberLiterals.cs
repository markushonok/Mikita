using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class NumberLiterals
	{
		public static LengthRecord<T> km<T>(this T number)
			where T : INumber<T>
			=> new(InMeters: number * T.CreateChecked(1000));
		
		public static LengthRecord<T> m<T>(this T number)
			where T : INumber<T>
			=> new(InMeters: number);

		public static LengthRecord<T> cm<T>(this T number)
			where T : INumber<T>
			=> new(InMeters: number / T.CreateChecked(100));
		
		public static LengthRecord<T> mm<T>(this T number)
			where T : INumber<T>
			=> new(InMeters: number / T.CreateChecked(1000));
	}