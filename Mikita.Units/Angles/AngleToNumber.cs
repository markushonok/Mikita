namespace Mikita.Units.Angles;

public static class AngleToNumber
	{
		public static float deg(this Angle<float> angle)
			=> angle.Rad * (180 / MathF.PI);
		
		public static double deg(this Angle<double> angle)
			=> angle.Rad * (180 / System.Math.PI);
	}