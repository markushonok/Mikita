using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public partial record struct RotationRecord2D<T>
	(
		Angle<T> Horizontal,
		Angle<T> Vertical
	) 
	
	: Rotation2D<T>
	
	where T 
		: INumber<T>
		, IFloatingPointConstants<T>;