namespace Mikita.Math.Points;

public sealed record Point2D<T>
	(
		T X,
		T Y
	)
	: IPoint2D<T>;