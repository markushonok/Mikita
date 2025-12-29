namespace Mikita.Measurement.Motion.Speeds;

public record Speed<T>
	(
		T MetersPerSecond
	) 
	: ISpeed<T>;

public static class Speed;