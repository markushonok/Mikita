using System.Numerics;

namespace Mikita.Measurement.Lengths;

public interface Length<out T> where T : INumber<T>
	{
		T m { get; }
	}