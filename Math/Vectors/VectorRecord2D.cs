using System.Numerics;

namespace Mikita.Math.Vectors;

public readonly partial record struct VectorRecord2D<T>
	(
		T X,
		T Y
	)
	: Vector2D<T>
	where T : INumber<T>, IRootFunctions<T>;