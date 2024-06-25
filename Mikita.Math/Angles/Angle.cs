using System.Numerics;

namespace Mikita.Math.Angles;

public interface Angle<out T> 
	where T : INumber<T>
	{
		T Radians { get; }
	}