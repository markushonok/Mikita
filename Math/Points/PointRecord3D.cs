using System.Numerics;

namespace Mikita.Math.Points;

public record struct PointRecord3D<T>
	(
		T X, 
		T Y, 
		T Z
	) 
	: Point3D<T>
	where T : INumber<T>;