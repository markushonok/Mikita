using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthToNumber
	{
		public static T km<T>(this Length<T> length)
			where T : INumber<T>, IDivisionOperators<T, int, T>
			=> length.m / 1000;
		
		public static T cm<T>(this Length<T> length)
			where T : INumber<T>, IMultiplyOperators<T, int, T>
			=> length.m * 100;
		
		public static T mm<T>(this Length<T> length)
			where T : INumber<T>, IMultiplyOperators<T, int, T>
			=> length.m * 1000;
	}