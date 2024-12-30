using System.Numerics;

namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<T> 
	where T : INumber<T>, IRootFunctions<T>
	{
		Speed<T> X { get; }
		
		Speed<T> Y { get; }
		
		Speed<T> Z { get; }
	}