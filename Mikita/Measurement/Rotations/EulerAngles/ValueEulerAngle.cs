using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record struct ValueEulerAngle<T>
	(
		Angle<T> X,
		Angle<T> Y,
		Angle<T> Z
	)
	: IEulerAngle<T>

	where T:
		struct,
		INumber<T>,
		IFloatingPointConstants<T>;