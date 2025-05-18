namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static bool operator <
			(
				Angle<T> comparandum,
				Angle<T> comparatum
			)
			=> comparandum.InRadians < comparatum.InRadians;

		static bool operator >
			(
				Angle<T> comparandum,
				Angle<T> comparatum
			)
			=> comparandum.InRadians > comparatum.InRadians;
	}