using System.Numerics;

namespace Mikita.Measurement.Angles;

public partial interface Angle<out T> 

	where T
		: INumber<T>
		, IFloatingPointConstants<T>

	{
		T InRadians { get; }
		
		T InDegrees 
			=> InRadians * (T.CreateChecked(180) / T.Pi);
	}