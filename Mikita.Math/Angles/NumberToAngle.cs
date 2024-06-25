using System.Numerics;

namespace Mikita.Math.Angles;

public static class NumberToAngle
	{
		public static SnapshotAngle<T> Radians<T>(this T radians)
			where T : INumber<T>
			=> new(radians);
		
		public static SnapshotAngle<float> Degrees(this float degrees)
			=> new(degrees * (MathF.PI / 180F));
		
		public static SnapshotAngle<double> Degrees(this double degrees)
			=> new(degrees * (System.Math.PI / 180));
	}