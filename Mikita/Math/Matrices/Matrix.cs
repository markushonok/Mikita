namespace Mikita.Math.Matrices;

public static class Matrix
	{
		public static ValueMatrix2D<float> With
			(
				float[,] array
			)
			=> new(array);
	}