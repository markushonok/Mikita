using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial interface IEulerAngle<out T>

	where T:
		INumber<T>,
		IFloatingPointConstants<T>

	{
		Angle<T> X { get; }

		Angle<T> Y { get; }

		Angle<T> Z { get; }

		EulerOrder Order { get; }
	}