namespace Mikita.Measurement.Lengths;

public partial interface ILength<out T>
	{
		static Length<T> operator +
			(
				ILength<T> augend,
				ILength<T> addend
			)
			=> new(augend.Meters + addend.Meters);

		static Length<T> operator -
			(
				ILength<T> minuend,
				ILength<T> subtrahend
			)
			=> new(minuend.Meters - subtrahend.Meters);

		static Length<T> operator *
			(
				ILength<T> multiplicand,
				T multiplier
			)
			=> Length.FromMeters(multiplicand.Meters * multiplier);

		static Length<T> operator /
			(
				ILength<T> dividend,
				T divisor
			)
			=> Length.FromMeters(dividend.Meters / divisor);
	}