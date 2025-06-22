namespace Mikita.Math.Points;

public interface IPoint3D<out T>
	{
		T X { get; }

		T Y { get; }

		T Z { get; }
	}