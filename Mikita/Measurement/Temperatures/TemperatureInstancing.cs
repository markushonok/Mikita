using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureInstancing
	{
		extension<T>(Temperature)
			where T: INumber<T>
			{
				public static Temperature<T> FromKelvins
					(
						T number
					)
					=> new(number);

				public static Temperature<T> FromCelsius
					(
						T number
					)
					=> new(number + Temperature<T>.WaterFreezingPoint.Kelvin);

				/// <summary>
				/// (F - 32) * 5/9 + 273.150
				/// </summary>
				/// <param name="number"></param>
				/// <returns></returns>
				public static Temperature<T> FromFarengheit
					(
						T number
					)
					{
						var thirtyTwo = T.CreateChecked(32);
						var fiveNinths = T.CreateChecked(5.0 / 9.0);
						var kelvins = (number - thirtyTwo) * fiveNinths + Temperature<T>.WaterFreezingPoint.Kelvin;
						return Temperature.FromKelvins(kelvins);
					}
			}
	}