using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record EulerAngle<T>
	(
		Angle<T> X,
		Angle<T> Y,
		Angle<T> Z
	)
	: IEulerAngle<T>

	where T:
		INumber<T>,
		IFloatingPointConstants<T>;