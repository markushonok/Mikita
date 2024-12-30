using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthToNumber
	{
		internal static T InKilometers<T>(this Length<T> length)
			where T : INumber<T>
			=> length.InMeters * T.CreateChecked(0.001);
		
		internal static T InCentimeters<T>(this Length<T> length)
			where T : INumber<T>
			=> length.InMeters * T.CreateChecked(100);
		
		internal static T InMillimeters<T>(this Length<T> length)
			where T : INumber<T>
			=> length.InMeters * T.CreateChecked(1000);
	}