namespace Mikita.Measurement.Angles;

public partial record struct AngleRecord<T>
	{
		public static AngleRecord<T> operator +
			(
				AngleRecord<T> left,
				Angle<T> right
			)
			=> (Angle<T>) left + right;

		public static Angle<T> operator -
			(
				AngleRecord<T> left,
				Angle<T> right
			)
			=> (Angle<T>) left - right;

		public static Angle<T> operator *
			(
				AngleRecord<T> left,
				T right
			)
			=> (Angle<T>) left * right;

		public static Angle<T> operator *
			(
				AngleRecord<T> left,
				Angle<T> right
			)
			=> (Angle<T>) left * right;

		public static Angle<T> operator /
			(
				AngleRecord<T> left,
				Angle<T> right
			)
			=> (Angle<T>) left / right;
		
		public static Angle<T> operator /
			(
				AngleRecord<T> left,
				T right
			)
			=> (Angle<T>) left / right;
	}