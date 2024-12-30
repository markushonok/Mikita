namespace Mikita.Measurement.Lengths;

public partial interface Length<out T>
	{
		static LengthRecord<T> operator +
			(
				Length<T> left, 
				Length<T> right
			)
			=> new(left.InMeters + right.InMeters);
		
		static LengthRecord<T> operator -
			(
				Length<T> left, 
				Length<T> right
			)
			=> new(left.InMeters - right.InMeters);
		
		static LengthRecord<T> operator *
			(
				Length<T> left, 
				T right
			)
			=> left * right.m();
		
		static LengthRecord<T> operator *
			(
				Length<T> left, 
				Length<T> right
			)
			=> new(left.InMeters * right.InMeters);

		static LengthRecord<T> operator /
			(
				Length<T> left,
				T right
			)
			=> left / right.m();
		
		static LengthRecord<T> operator /
			(
				Length<T> left, 
				Length<T> right
			)
			=> new(left.InMeters / right.InMeters);
	}