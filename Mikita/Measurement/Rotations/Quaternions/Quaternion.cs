namespace Mikita.Measurement.Rotations.Quaternions;

public readonly record struct Quaternion<T>
	(
		T W,
		T X,
		T Y,
		T Z
	)
	: IQuaternion<T>;