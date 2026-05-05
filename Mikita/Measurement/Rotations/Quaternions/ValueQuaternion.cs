using System.Numerics;

namespace Mikita.Measurement.Rotations.Quaternions;

public readonly partial struct ValueQuaternion<T>
	(
		T w,
		T x,
		T y,
		T z
	)
	: IQuaternion<T>
	where T: INumber<T>
	{
		public T W => w;

		public T X => x;

		public T Y => y;

		public T Z => z;

	}