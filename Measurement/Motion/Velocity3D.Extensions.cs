namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<T>
	{
		Speed<T> Speed
			=> this.Speed();
	}