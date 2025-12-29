namespace Mikita.Measurement.Lengths;

public partial interface ILength<out T>
	{
		static bool operator <
			(
				ILength<T> comparandum,
				ILength<T> comparatum
			)
			=> comparandum.Meters < comparatum.Meters;

		static bool operator <=
			(
				ILength<T> comparandum,
				ILength<T> comparatum
			)
			=> comparandum.Meters <= comparatum.Meters;

		static bool operator >
			(
				ILength<T> comparandum,
				ILength<T> comparatum
			)
			=> comparandum.Meters > comparatum.Meters;

		static bool operator >=
			(
				ILength<T> comparandum,
				ILength<T> comparatum
			)
			=> comparandum.Meters >= comparatum.Meters;
	}