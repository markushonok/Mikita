using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthToNumber
	{
		public static T InKilometers<T>(this ILength<T> length)
			where T : struct, INumber<T>
			=> length.Meters() * T.CreateChecked(0.001);
		
		public static T InCentimeters<T>(this ILength<T> length)
			where T : struct, INumber<T>
			=> length.Meters() * T.CreateChecked(100);
		
		public static T InMillimeters<T>(this ILength<T> length)
			where T : struct, INumber<T>
			=> length.Meters() * T.CreateChecked(1000);
	}