using System.Numerics;

namespace Mikita.Math.Points;

public record struct PointRecord2D<T>
	(
		T X, 
		T Y
	) 
	: Point2D<T>
	where T : INumber<T>;