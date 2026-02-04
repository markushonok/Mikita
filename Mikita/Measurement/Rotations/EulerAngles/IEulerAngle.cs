using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

/// <summary>
/// Represents Euler angles for rotation in 3D space.
/// </summary>
public interface IEulerAngle<out T>
	{
		IAngle<T> X { get; }

		IAngle<T> Y { get; }

		IAngle<T> Z { get; }
	}