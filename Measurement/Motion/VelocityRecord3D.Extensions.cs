namespace Mikita.Measurement.Motion;

public sealed partial record VelocityRecord3D<T>
	{
		public Speed<T> Speed
			=> this.Speed();
	}