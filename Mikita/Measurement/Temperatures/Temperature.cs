namespace Mikita.Measurement.Temperatures;

public sealed record Temperature<T>
	(
		T Kelvin
	)
	: ITemperature<T>;

public static class Temperature;