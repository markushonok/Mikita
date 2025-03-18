namespace Mikita.Math.Matrices;

public static class ValuesMatrix
	{
		public static ValueMatrix2D<T> With<T>
			(
				T[,] array
			)
			where T: struct
			=> new(array);
	}