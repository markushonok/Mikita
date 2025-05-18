using System.Numerics;

namespace Mikita.Math.Vectors;

public record struct ValueVector3D<T>
	(
		T X,
		T Y,
		T Z
	)
	: IVector3D<T>
	where T: struct, INumber<T>, IRootFunctions<T>;