using Mikita.Math.Points;
using System.Numerics;

namespace Mikita.Math.Shapes.Quads;

public record struct QuadRecord<T>
	(
		Point2D<T> TopLeft,
		Point2D<T> TopRight,
		Point2D<T> BottomLeft,
		Point2D<T> BottomRight
	) 
	: Quad<T> 
	where T : INumber<T>;