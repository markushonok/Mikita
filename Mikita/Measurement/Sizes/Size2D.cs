using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public sealed record Size2D<T>
	(
		ILength<T> Width,
		ILength<T> Height
	)
	: ISize2D<T>
	where T : INumber<T>;