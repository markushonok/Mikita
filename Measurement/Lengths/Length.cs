using System.Numerics;

namespace Mikita.Measurement.Lengths;

public partial interface Length<out T> 
	where T : INumber<T>
	{
		T InMeters { get; }
	}