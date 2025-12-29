namespace Mikita.Math.Vectors.Spatial;

public readonly record struct ValueVector3D<T>
	(
		T X,
		T Y,
		T Z
	)
	: IVector3D<T>
	where T: struct;