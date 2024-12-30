using System.Numerics;

namespace Mikita.Measurement.Motion;

public interface Velocity2D<T>

	where T: 
		INumber<T>, 
		IRootFunctions<T>

	{
		Speed<T> X { get; }
		
		Speed<T> Y { get; }
	}