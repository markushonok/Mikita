namespace Mikita.Measurement.Angles;

public partial record struct AngleRecord<T>
	{
		public static bool operator <
			(
				AngleRecord<T> comparandum,
				Angle<T> comparatum
			)
			=> (Angle<T>) comparandum < comparatum;

		public static bool operator >
			(
				AngleRecord<T> comparandum,
				Angle<T> comparatum
			)
			=> (Angle<T>) comparandum > comparatum;
	}