using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public readonly partial record struct ValuePosition3D<T>
	(
		ValueLength<T> X,
		ValueLength<T> Y,
		ValueLength<T> Z
	)
	: IPosition3D<T>
	where T: struct, INumber<T>
	{
		ILength<T> IPosition3D<T>.X
			=> X;

		ILength<T> IPosition3D<T>.Y
			=> Y;

		ILength<T> IPosition3D<T>.Z
			=> Z;
	}