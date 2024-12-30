using Mikita.Math.Vectors;
using System.Numerics;

namespace Mikita.Math.Shapes.Quads;

public record struct QuadRecord<T>
	(
		Vector2D<T> TopLeft,
		Vector2D<T> TopRight,
		Vector2D<T> BottomLeft,
		Vector2D<T> BottomRight
	) 
	: Quad<T> 
	where T : INumber<T>, IRootFunctions<T>;