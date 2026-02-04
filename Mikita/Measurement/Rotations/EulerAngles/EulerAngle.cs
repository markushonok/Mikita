using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record EulerAngle<T>
	(
		IAngle<T> X,
		IAngle<T> Y,
		IAngle<T> Z
	)
	: IEulerAngle<T>

	where T:
		INumber<T>,
		IFloatingPointConstants<T>;