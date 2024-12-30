using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public record struct PositionRecord2D<T>
	(
		Length<T> X, 
		Length<T> Y
	) 
	: Position2D<T> 
	where T : INumber<T>;