using System.Numerics;

namespace Mikita.Measurement.Angles;

public interface Angle<out T> where T : INumber<T>
	{
		T rad { get; }
	}