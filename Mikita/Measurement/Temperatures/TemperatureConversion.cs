using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureConversion
	{
		extension<T>(ITemperature<T> temperature)
			where T: INumber<T>
			{
				public T Celsius
					=> temperature.Kelvin - Temperature<T>.WaterFreezingPoint.Kelvin;

				/// <summary>
				/// (K - 273.15) * 9/5 + 32
				/// </summary>
				public T Fahrenheit
					{
						get
							{
								var factor = T.CreateChecked(1.8);
								var offsetF = T.CreateChecked(32);
								return (temperature.Celsius * factor) + offsetF;
							}
					}
			}
	}