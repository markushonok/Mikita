namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static AngleRecord<T> operator +
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> Angle.Radians(left.InRadians + right.InRadians);
		
		static AngleRecord<T> operator -
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> Angle.Radians(left.InRadians - right.InRadians);
		
		static AngleRecord<T> operator *
			(
				Angle<T> left,
				T right
			)
			=> Angle.Radians(left.InRadians * right);
		
		static AngleRecord<T> operator *
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> Angle.Radians(left.InRadians * right.InRadians);
		
		static AngleRecord<T> operator /
			(
				Angle<T> left,
				T right
			)
			=> Angle.Radians(left.InRadians / right);
		
		static AngleRecord<T> operator /
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> Angle.Radians(left.InRadians / right.InRadians);
	}
