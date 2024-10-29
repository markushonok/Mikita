using System.Numerics;

namespace Mikita.Math.Points;

public interface Point2D<out T> 
	where T : INumber<T>
	{
		T X { get; }
		
		T Y { get; }
	}