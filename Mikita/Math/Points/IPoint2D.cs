namespace Mikita.Math.Points;

public interface IPoint2D<out T>
	{
		T X { get; }

		T Y { get; }
	}