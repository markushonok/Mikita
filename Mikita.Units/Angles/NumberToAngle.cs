using System.Numerics;

namespace Mikita.Units.Angles;

public static class NumberToAngle
	{
		public static AngleRecord<T> rad<T>(this T number)
			where T : INumber<T>
			=> new(number);
		
		public static AngleRecord<float> deg(this float number)
			=> new(number * (MathF.PI / 180F));
		
		public static AngleRecord<double> deg(this double number)
			=> new(number * (Math.PI / 180));
		
		public static AngleRecord<float> turns(this float number)
			=> new(number * 2 * MathF.PI);
		
		public static AngleRecord<double> turns(this double number)
			=> new(number * 2 * Math.PI);
	}