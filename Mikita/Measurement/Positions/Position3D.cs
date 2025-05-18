using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public sealed partial record Position3D<T>
	(
		ILength<T> X, 
		ILength<T> Y,
		ILength<T> Z
	) 
	: IPosition3D<T>
	where T : INumber<T>;