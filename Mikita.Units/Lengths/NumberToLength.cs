using System.Numerics;

namespace Mikita.Units.Lengths;

public static class NumberToLength
	{
		public static LengthRecord<T> km<T>(this T number)
			where T : INumber<T>, IMultiplyOperators<T, int, T>
			=> new(m: number * 1000);

		public static LengthRecord<T> cm<T>(this T number)
			where T : INumber<T>, IDivisionOperators<T, int, T>
			=> new(m: number / 100);
		
		public static LengthRecord<T> mm<T>(this T number)
			where T : INumber<T>, IDivisionOperators<T, int, T>
			=> new(m: number / 1000);
	}