using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureConstant
	{
		extension<T>(Temperature<T>)
			where T: INumber<T>
			{
				public static ITemperature<T> WaterFreezingPoint
					=> Temperature.FromKelvins(T.CreateChecked(273.15));

				public static ITemperature<T> AbsoluteZero
					=> Temperature.FromKelvins(T.Zero);
			}
	}