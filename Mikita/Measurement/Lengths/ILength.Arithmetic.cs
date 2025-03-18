namespace Mikita.Measurement.Lengths;

public partial interface ILength<out T>
	{
		static Length<T> operator +
			(
				ILength<T> left,
				ILength<T> right
			)
			=> new(left.Meters() + right.Meters());
		
		static Length<T> operator -
			(
				ILength<T> left,
				ILength<T> right
			)
			=> new(left.Meters() - right.Meters());
		
		static Length<T> operator *
			(
				ILength<T> left,
				T right
			)
			=> Length.FromMeters(left.Meters() * right);

		static Length<T> operator /
			(
				ILength<T> left,
				T right
			)
			=> Length.FromMeters(left.Meters() / right);
	}