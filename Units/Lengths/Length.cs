using System.Numerics;

namespace Mikita.Units.Lengths;

public interface Length<out T> where T : INumber<T>
	{
		T m { get; }
	}