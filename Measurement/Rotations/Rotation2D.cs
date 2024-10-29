using Mikita.Aliases;
using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>

	where T
		: INumber<T>
		, IFloatingPointConstants<T>

	{
		Angle<T> X { get; }
		
		Angle<T> Y { get; }
	}
	
public partial interface Rotation2D 
	
	: Rotation2D<float>
	, Alias<Rotation2D<float>, Rotation2D>;