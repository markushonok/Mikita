using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureLiterals
	{
		extension<T>(T number)
			where T: struct, INumber<T>
			{
				public ValueTemperature<T> K
					=> new(Kelvin: number);

				public Temperature<T> C
					=> Temperature.FromCelsius(number);

				public Temperature<T> F
					=> Temperature.FromFarengheit(number);
			}
	}
