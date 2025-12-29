namespace Mikita.Measurement.Motion.Accelerations;

public sealed record Acceleration<T>
	(
		T MetersPerSecond2
	)
	: IAcceleration<T>;

public static class Acceleration;