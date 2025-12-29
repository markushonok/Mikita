using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public interface IAabb3D<out T>
	where T: INumber<T>, IRootFunctions<T>
	{
		IVector3D<T> Min { get; }

		IVector3D<T> Max { get; }
	}