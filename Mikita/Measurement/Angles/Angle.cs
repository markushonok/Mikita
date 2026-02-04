namespace Mikita.Measurement.Angles;

public record struct Angle<T>
	(
		T InRadians
	)
	: IAngle<T>;

public static class Angle;