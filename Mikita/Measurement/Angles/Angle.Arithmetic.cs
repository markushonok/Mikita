namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static AngleRecord<T> operator +
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians + right.InRadians).AsAngleInRadians();
		
		static AngleRecord<T> operator -
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians - right.InRadians).AsAngleInRadians();
		
		static AngleRecord<T> operator *
			(
				Angle<T> left,
				T right
			)
			=> left * right.AsAngleInRadians();
		
		static AngleRecord<T> operator *
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians * right.InRadians).AsAngleInRadians();
		
		static AngleRecord<T> operator /
			(
				Angle<T> left,
				T right
			)
			=> left / right.AsAngleInRadians();
		
		static AngleRecord<T> operator /
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians / right.InRadians).AsAngleInRadians();
	}