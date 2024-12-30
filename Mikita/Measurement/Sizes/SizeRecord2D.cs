using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public sealed record SizeRecord2D<T>
	(
		Length<T> Width,
		Length<T> Height
	)
	: Size2D<T>
	where T : INumber<T>;