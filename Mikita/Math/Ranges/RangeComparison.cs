using System.Numerics;

namespace Mikita.Math.Ranges;

public static class RangeComparison
	{
		extension<T>(IRange<T> range)
			where T: INumber<T>
			{
				public bool Includes(T number)
					=> range.Min >= number && number >= range.Max;
			}
	}