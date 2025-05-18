namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static AngleRecord<T> operator +
			(
				Angle<T> augend, 
				Angle<T> addend
			)
			=> Angle.Radians(augend.InRadians + addend.InRadians);
		
		static AngleRecord<T> operator -
			(
				Angle<T> minuend, 
				Angle<T> subtrahend
			)
			=> Angle.Radians(minuend.InRadians - subtrahend.InRadians);
		
		static AngleRecord<T> operator *
			(
				Angle<T> multiplicand,
				T multiplier
			)
			=> Angle.Radians(multiplicand.InRadians * multiplier);
		
		static AngleRecord<T> operator *
			(
				Angle<T> multiplicand, 
				Angle<T> multiplier
			)
			=> Angle.Radians(multiplicand.InRadians * multiplier.InRadians);
		
		static AngleRecord<T> operator /
			(
				Angle<T> dividend,
				T divisor
			)
			=> Angle.Radians(dividend.InRadians / divisor);
		
		static AngleRecord<T> operator /
			(
				Angle<T> dividend, 
				Angle<T> divisor
			)
			=> Angle.Radians(dividend.InRadians / divisor.InRadians);
	}