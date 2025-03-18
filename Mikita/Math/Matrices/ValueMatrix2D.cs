namespace Mikita.Math.Matrices;

public readonly struct ValueMatrix2D<T>
	(
		T[,] source
	)
	: IMatrix2D<T> where T: struct
	{
		public T this[int x, int y]
			=> source[x, y];
	}