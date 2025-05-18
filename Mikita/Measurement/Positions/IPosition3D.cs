using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public partial interface IPosition3D<out T>
	where T : INumber<T>
	{
		ILength<T> X { get; }
		
		ILength<T> Y { get; }
		
		ILength<T> Z { get; }
	}