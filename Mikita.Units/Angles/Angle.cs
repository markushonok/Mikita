using System.Numerics;

namespace Mikita.Units.Angles;

public interface Angle<out T> where T : INumber<T>
	{
		T Rad { get; }
	}