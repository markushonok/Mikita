using System.Numerics;

namespace Mikita.Math.Vectors;

public record struct VectorRecord3D<T>
	(
		T X, 
		T Y, 
		T Z
	) 
	: Vector3D<T>
	where T : INumber<T>, IRootFunctions<T>;