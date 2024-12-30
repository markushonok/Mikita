using System.Numerics;

namespace Mikita.Math.Vectors;

public partial interface Vector2D<out T> 
	
	where T: 
		INumber<T>, 
		IRootFunctions<T>

	{
		T X { get; }
		
		T Y { get; }
	}