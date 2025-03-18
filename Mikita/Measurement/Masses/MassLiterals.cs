using System.Numerics;

namespace Mikita.Measurement.Masses;

public static class MassLiterals
	{
		public static ValueMass<T> t<T>(this T number)
			where T : struct, INumber<T>
			=> new(kilograms: number * T.CreateChecked(1000));
		
		public static ValueMass<T> kg<T>(this T number)
			where T : struct, INumber<T>
			=> new(kilograms: number);

		public static ValueMass<T> g<T>(this T number)
			where T : struct, INumber<T>
			=> new(kilograms: number / T.CreateChecked(1000));
	}