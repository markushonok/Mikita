using System.Numerics;

namespace Mikita.Measurement.Angles;

public interface IAngle<out T>
	{
		T InRadians { get; }
	}