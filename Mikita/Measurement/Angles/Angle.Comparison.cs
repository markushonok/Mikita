namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static bool operator <
			(
				Angle<T> left,
				Angle<T> right
			)
			=> left.InRadians < right.InRadians;

		static bool operator >
			(
				Angle<T> left, 
				Angle<T> right
			)
			=> left.InRadians > right.InRadians;
	}