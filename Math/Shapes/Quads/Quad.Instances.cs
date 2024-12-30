using Mikita.Math.Vectors;

namespace Mikita.Math.Shapes.Quads;

public partial interface Quad<out T>
	{
		static QuadRecord<T> WithVertices
			(
				Vector2D<T> topLeft,
				Vector2D<T> topRight,
				Vector2D<T> bottomLeft,
				Vector2D<T> bottomRight
			)
			=> new(topLeft, topRight, bottomLeft, bottomRight);
	}