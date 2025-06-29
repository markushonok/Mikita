using Mikita.Measurement.Lengths;
using System;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public readonly partial struct ValuePosition3D<T>
	(
		ValueLength<T> x,
		ValueLength<T> y,
		ValueLength<T> z
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

		public ValueLength<T> X => x;

		public ValueLength<T> Y => y;

		public ValueLength<T> Z => z;
	}