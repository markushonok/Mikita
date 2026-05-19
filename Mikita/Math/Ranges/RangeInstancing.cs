using System.Numerics;

namespace Mikita.Math.Ranges;

public static class RangeInstancing
	{
		extension<T>(T value)
			where T: IMinMaxValue<T>
			{
				public static IRange<T> Range
					=> Range.Between(T.MinValue, T.MaxValue);
			}

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