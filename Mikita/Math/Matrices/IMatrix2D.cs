namespace Mikita.Math.Matrices;

public interface IMatrix2D<out T>
	{
		T this[int x, int y] { get; }
	}