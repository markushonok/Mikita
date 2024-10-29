namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static Angle<T> operator +
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians + right.InRadians).AsAngleInRadians();
		
		static Angle<T> operator -
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians - right.InRadians).AsAngleInRadians();
		
		static Angle<T> operator *
			(
				Angle<T> left,
				T right
			)
			=> left * right.AsAngleInRadians();
		
		static Angle<T> operator *
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians * right.InRadians).AsAngleInRadians();
		
		static Angle<T> operator /
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> (left.InRadians / right.InRadians).AsAngleInRadians();
	}