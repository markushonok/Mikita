using Mikita.Math.Vectors;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public readonly record struct ValueAabb3D<T>
	(
		ValueVector3D<T> Min,
		ValueVector3D<T> Max
	)
	: IAabb3D<T>
	where T: struct, INumber<T>, IRootFunctions<T>
	{
		IVector3D<T> IAabb3D<T>.Min => Min;

		IVector3D<T> IAabb3D<T>.Max => Max;
	}