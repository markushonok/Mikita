using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public record struct Position2D<T>
	(
		ILength<T> X, 
		ILength<T> Y
	) 
	: IPosition2D<T>
	where T : INumber<T>;