using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public record struct ValueEulerAngle<T>
	(
		IAngle<T> X,
		IAngle<T> Y,
		IAngle<T> Z
	)
	: IEulerAngle<T>

	where T:
		struct,
		INumber<T>,
		IFloatingPointConstants<T>;