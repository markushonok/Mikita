using Mikita.Math.Ranges;
using System.Numerics;

namespace Mikita.Measurement.Temperatures;

public static class TemperatureComparison
	{
		extension<T>(IRange<ITemperature<T>> range)
			where T: INumber<T>
			{
				public bool Includes
					(
						ITemperature<T> temperature
					)
					=> range.Min <= temperature
						&& temperature <= range.Max;
			}

		extension<T>(ITemperature<T>)
			where T: INumber<T>
			{
				public static bool operator <
					(
						ITemperature<T> comparandum,
						ITemperature<T> comparatum
					)
					=> comparandum.Kelvin < comparatum.Kelvin;

				public static bool operator <=
					(
						ITemperature<T> comparandum,
						ITemperature<T> comparatum
					)
					=> comparandum.Kelvin <= comparatum.Kelvin;

				public static bool operator >
					(
						ITemperature<T> comparandum,
						ITemperature<T> comparatum
					)
					=> comparandum.Kelvin > comparatum.Kelvin;

				public static bool operator >=
					(
						ITemperature<T> comparandum,
						ITemperature<T> comparatum
					)
					=> comparandum.Kelvin >= comparatum.Kelvin;
			}
	}