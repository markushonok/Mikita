using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureMath
	{
		extension<T>(ITemperature<T> temperature)
			where T: INumber<T>
			{
				public Temperature<T> MathAbs
					=> new
						(
							T.Abs(temperature.Kelvin)
						);
			}

		extension<T>(ITemperature<T>)
			where T: INumber<T>
			{
				public static Temperature<T> operator +
					(
						ITemperature<T> augend,
						ITemperature<T> addend
					)
					=> new(augend.Kelvin + addend.Kelvin);

				public static Temperature<T> operator -
					(
						ITemperature<T> minuend,
						ITemperature<T> subtrahend
					)
					=> new(minuend.Kelvin - subtrahend.Kelvin);

				public static Temperature<T> operator *
					(
						ITemperature<T> multiplicand,
						T multiplier
					)
					=> Temperature.FromKelvins(multiplicand.Kelvin * multiplier);

				public static Temperature<T> operator /
					(
						ITemperature<T> dividend,
						T divisor
					)
					=> Temperature.FromKelvins(dividend.Kelvin / divisor);
			}
	}