using System.Numerics;

namespace Mikita.Measurement.Lengths;

public readonly record struct LengthRecord<T>(T m)
	: Length<T>
	where T : INumber<T>;