using System.Numerics;

namespace Mikita.Measurement.Angles;

public record struct AngleRecord<T>(T rad) 
	: Angle<T>
	where T : INumber<T>;