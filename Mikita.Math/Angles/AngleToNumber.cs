namespace Mikita.Math.Angles;

public static class AngleToNumber
	{
		public static float Degrees(this Angle<float> angle)
			=> angle.Radians * (180 / MathF.PI);
		
		public static double Degrees(this Angle<double> angle)
			=> angle.Radians * (180 / System.Math.PI);
	}