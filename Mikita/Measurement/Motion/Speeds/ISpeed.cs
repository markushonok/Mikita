namespace Mikita.Measurement.Motion.Speeds;

public interface ISpeed<out T>
	{
		T MetersPerSecond { get; }
	}