using System.Numerics;

namespace Mikita.Measurement.Lengths;

public partial interface ILength<out T>
	where T : INumber<T>
	{
		T Meters();
	}