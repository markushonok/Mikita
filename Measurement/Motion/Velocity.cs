using System.Numerics;

namespace Mikita.Measurement.Motion;

public static partial class Velocity
	{
		public static VelocityRecord3D<T> Of<T>
			(
				Speed<T> x, 
				Speed<T> y, 
				Speed<T> z
			) 
			where T : INumber<T>, IRootFunctions<T>
			=> new(x, y, z);
	}