namespace Mikita.Measurement.Angles;

public partial record struct AngleRecord<T>
	{
		public static bool operator <
			(
				AngleRecord<T> left,
				Angle<T> right
			)
			=> (Angle<T>) left < right;

		public static bool operator >
			(
				AngleRecord<T> left, 
				Angle<T> right
			)
			=> (Angle<T>) left > right;
	}