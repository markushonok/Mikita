using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public partial interface Position2D<out T>
	where T : INumber<T>
	{
		Length<T> X { get; }
		
		Length<T> Y { get; }
	}