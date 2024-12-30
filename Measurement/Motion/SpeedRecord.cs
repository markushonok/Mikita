using System.Numerics;

namespace Mikita.Measurement.Motion;

public record SpeedRecord<T>
	(
		T InMetersPerSecond
	) 
	: Speed<T> 
	where T: INumber<T>, IRootFunctions<T>;