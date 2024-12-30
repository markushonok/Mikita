using System.Numerics;

namespace Mikita.Measurement.Motion;

public sealed partial record VelocityRecord3D<T>
	(
		Speed<T> X, 
		Speed<T> Y, 
		Speed<T> Z
	) 
	: Velocity3D<T> 
	where T : INumber<T>, IRootFunctions<T>;