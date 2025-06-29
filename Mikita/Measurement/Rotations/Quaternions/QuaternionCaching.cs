using System.Numerics;

namespace Mikita.Measurement.Rotations.Quaternions;

public static class QuaternionCaching
	{
		public static ValueQuaternion<T> Snapshot<T>
			(
				this IQuaternion<T> rotation
			)
			where T: INumber<T>
			=> new
				(
					rotation.X,
					rotation.Y,
					rotation.Z,
					rotation.W
				);
	}