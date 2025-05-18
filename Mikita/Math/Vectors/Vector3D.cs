using System.Numerics;

namespace Mikita.Math.Vectors;

public sealed partial record Vector3D<T>
	(
		T X, 
		T Y, 
		T Z
	) 
	: IVector3D<T>
	where T : INumber<T>, IRootFunctions<T>;