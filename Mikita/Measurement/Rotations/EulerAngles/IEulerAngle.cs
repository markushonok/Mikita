using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

/// <summary>
/// Represents Euler angles for rotation in 3D space.
/// </summary>
public partial interface IEulerAngle<out T>

	where T:
		INumber<T>,
		IFloatingPointConstants<T>

	{
		Angle<T> X { get; }

		Angle<T> Y { get; }

		Angle<T> Z { get; }
	}