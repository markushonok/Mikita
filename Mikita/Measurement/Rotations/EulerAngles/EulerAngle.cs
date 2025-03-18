using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record struct EulerAngle<T>
	(
		Angle<T> X,
		Angle<T> Y,
		Angle<T> Z,
		EulerOrder Order
	) 
	
	: IEulerAngle<T>
	
	where T 
		: INumber<T>
		, IFloatingPointConstants<T>;