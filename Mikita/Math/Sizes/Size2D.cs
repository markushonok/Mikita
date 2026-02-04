namespace Mikita.Math.Sizes;

public sealed partial class Size2D<T>
	(
		T x,
		T y
	)
	: ISize2D<T>
	where T: notnull
	{
		public T X => x;

		public T Y => y;
	}

public static class Size2D;