using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>

	where T
		: INumber<T>
		, IFloatingPointConstants<T>

	{
		Angle<T> Horizontal { get; }
		
		Angle<T> Vertical { get; }
	}