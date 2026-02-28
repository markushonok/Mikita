namespace Mikita.Math.Ranges;

public sealed record Range<T>
	(
		T Min,
		T Max
	)
	: IRange<T>;

public static class Range;