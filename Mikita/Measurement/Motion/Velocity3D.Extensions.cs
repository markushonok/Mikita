namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<out T>
	{
		Speed<T> Speed
			=> this.Speed();
	}