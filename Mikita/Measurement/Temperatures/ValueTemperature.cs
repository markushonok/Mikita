namespace Mikita.Measurement.Temperatures;

public readonly record struct ValueTemperature<T>
	(
		T Kelvin
	)
	: ITemperature<T>
	where T: struct;