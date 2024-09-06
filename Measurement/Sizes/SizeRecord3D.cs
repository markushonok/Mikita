using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public sealed record SizeRecord3D<T>
	(
		Length<T> Width,
		Length<T> Height,
		Length<T> Depth
	)
	: Size3D<T>
	where T : INumber<T>;