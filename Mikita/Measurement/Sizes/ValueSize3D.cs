using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public readonly struct ValueSize3D<T>
	(
		ValueLength<T> width,
		ValueLength<T> height,
		ValueLength<T> depth
	)
	: ISize3D<T>
	where T : struct, INumber<T>
	{
		public ValueLength<T> Width
			=> width;

		ILength<T> ISize3D<T>.Width
			=> width;

		public ValueLength<T> Height
			=> height;

		ILength<T> ISize3D<T>.Height
			=> height;

		public ValueLength<T> Depth
			=> depth;

		ILength<T> ISize3D<T>.Depth
			=> depth;
	}