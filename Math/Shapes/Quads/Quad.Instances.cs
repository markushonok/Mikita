using Mikita.Math.Points;

namespace Mikita.Math.Shapes.Quads;

public partial interface Quad<out T>
	{
		static QuadRecord<T> WithVertices
			(
				Point2D<T> topLeft,
				Point2D<T> topRight,
				Point2D<T> bottomLeft,
				Point2D<T> bottomRight
			)
			=> new(topLeft, topRight, bottomLeft, bottomRight);
	}