using System.Numerics;

namespace Mikita.Measurement.Rotations.Quaternions;

partial class Quaternion<T> where T: INumber<T>
	{
		public static Quaternion<T> Zero
			=> new
				(
					w: T.Zero,
					x: T.Zero,
					y: T.Zero,
					z: T.Zero
				);
	}

public static class Quaternion
	{
		public static Quaternion<float> Zero
			=> Quaternion<float>.Zero;
	}