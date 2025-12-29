namespace Mikita.Measurement.Motion.Accelerations;

public interface IAcceleration<out T>
	{
		T MetersPerSecond2 { get; }
	}