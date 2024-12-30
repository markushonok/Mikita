namespace Mikita.Measurement.Angles;

public partial interface Angle<out T>
	{
		static Angle<T> Zero
			=> T.Zero.AsAngleInRadians();
	}