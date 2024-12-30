namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<T>
	{
		public static VelocityRecord3D<T> Zero { get; }
			= new
				(
					Speed<T>.Zero, 
					Speed<T>.Zero, 
					Speed<T>.Zero
				);
	}