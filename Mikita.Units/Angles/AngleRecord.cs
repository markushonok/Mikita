using System.Numerics;

namespace Mikita.Units.Angles;

public record struct AngleRecord<T>(T rad) 
	: Angle<T>
	where T : INumber<T>;