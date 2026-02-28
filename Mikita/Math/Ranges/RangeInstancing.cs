namespace Mikita.Math.Ranges;

public static class RangeInstancing
	{
		extension(Range)
			{
				public static Range<T> Between<T>
					(
						T min,
						T max
					)
					=> new(min, max);

				public static Range<T> Point<T>
					(
						T position
					)
					=> new(Min: position, Max: position);
			}
	}