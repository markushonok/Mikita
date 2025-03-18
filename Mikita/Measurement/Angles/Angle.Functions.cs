using System.Numerics;

namespace Mikita.Measurement.Angles;

public static partial class Angle
	{
		public static Angle<T> Min<T>
			(
				Angle<T> left, 
				Angle<T> right
			) 
			where T : INumber<T>, IFloatingPointConstants<T> 
			=> left < right ? left : right;
		
		public static Angle<T> Max<T>
			(
				Angle<T> left, 
				Angle<T> right
			) 
			where T : INumber<T>, IFloatingPointConstants<T> 
			=> left < right ? left : right;
	}