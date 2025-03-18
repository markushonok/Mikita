using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public record struct PositionRecord3D<T>
	(
		ILength<T> X, 
		ILength<T> Y,
		ILength<T> Z
	) 
	: Position3D<T>
	where T : INumber<T>;