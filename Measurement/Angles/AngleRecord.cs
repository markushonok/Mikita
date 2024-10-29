using System.Numerics;

namespace Mikita.Measurement.Angles;

public record struct AngleRecord<T>(T InRadians)

	: Angle<T>

	where T 
		: INumber<T>
		, IFloatingPointConstants<T>;