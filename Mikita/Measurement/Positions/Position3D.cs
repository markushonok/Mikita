using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public sealed partial class Position3D<T>
	(
		ILength<T> x,
		ILength<T> y,
		ILength<T> z
	) 
	: IPosition3D<T>
	where T : INumber<T>
	{
		public ILength<T> X => x;

		public ILength<T> Y => y;

		public ILength<T> Z => z;
	}