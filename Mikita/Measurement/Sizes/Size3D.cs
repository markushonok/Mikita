using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public sealed record Size3D<T>
	(
		ILength<T> Width,
		ILength<T> Height,
		ILength<T> Depth
	)
	: ISize3D<T>
	where T : INumber<T>;