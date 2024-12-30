using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public record struct PositionRecord3D<T>
	(
		Length<T> X, 
		Length<T> Y,
		Length<T> Z
	) 
	: Position3D<T>
	where T : INumber<T>;