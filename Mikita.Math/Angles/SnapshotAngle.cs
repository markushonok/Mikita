using System.Numerics;

namespace Mikita.Math.Angles;

public record struct SnapshotAngle<T>
	(
		T Radians
	) 
	: Angle<T>
	where T : INumber<T>;