using System.Numerics;

namespace Mikita.Math.Points;

public interface Point3D<out T> 
	where T : INumber<T>
	{
		T X { get; }
		
		T Y { get; }
		
		T Z { get; }
	}