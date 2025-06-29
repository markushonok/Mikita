using System.Numerics;

namespace Mikita.Measurement.Rotations.Quaternions;

public interface IQuaternion<out T>
	where T: INumber<T>
	{
		T W { get; }

		T X { get; }

		T Y { get; }

		T Z { get; }
	}