using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public sealed record Aabb3D<T>
	(
		IVector3D<T> Min,
		IVector3D<T> Max
	)
	: IAabb3D<T>
	where T: INumber<T>, IRootFunctions<T>;