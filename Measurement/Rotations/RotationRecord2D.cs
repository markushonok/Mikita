using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public record struct RotationRecord2D<T>
	(
		Angle<T> X,
		Angle<T> Y
	) 
	
	: Rotation2D<T>
	
	where T 
		: INumber<T>
		, IFloatingPointConstants<T>;