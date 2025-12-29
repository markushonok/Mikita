namespace Mikita.Math.Vectors.Planar;

public sealed record Vector2D<T>
	(
		T X,
		T Y
	)
	: IVector2D<T>;