using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class NumberToAngle
	{
		public static AngleRecord<T> AsAngleInRadians<T>(this T number)
			
			where T 
			: INumber<T>
			, IFloatingPointConstants<T>
		
			=> new(InRadians: number);
		
		
		public static AngleRecord<T> ToAngleInDegrees<T>(this T number)
		
			where T 
			: INumber<T>
			, IFloatingPointConstants<T>
		
			=> new(InRadians: number * (T.Pi / T.CreateChecked(180)));
		
		
		public static AngleRecord<T> ToAngleInTurns<T>(this T number)
			
			where T 
			: INumber<T>
			, IFloatingPointConstants<T>
		
			=> new(InRadians: number * T.CreateChecked(2) * T.Pi);
	}