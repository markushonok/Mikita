namespace Mikita.Measurement.Temperatures;

public interface ITemperature<out T>
	{
		T Kelvin { get; }
	}