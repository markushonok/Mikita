using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class NumberToAngle
	{
		public static AngleRecord<T> rad<T>(this T number)
			where T : INumber<T>
			=> new(rad: number);
		
		public static AngleRecord<float> deg(this float number)
			=> new(rad: number * (MathF.PI / 180F));
		
		public static AngleRecord<double> deg(this double number)
			=> new(rad: number * (Math.PI / 180));
		
		public static AngleRecord<float> turns(this float number)
			=> new(rad: number * 2 * MathF.PI);
		
		public static AngleRecord<double> turns(this double number)
			=> new(rad: number * 2 * Math.PI);
	}