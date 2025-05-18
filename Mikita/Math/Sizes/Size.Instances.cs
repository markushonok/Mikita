namespace Mikita.Math.Sizes;

public static class Size
	{
		public static Size2D<T> Of<T>(T x, T y)
			=> new(x, y);
	}