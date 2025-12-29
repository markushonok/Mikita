using Mikita.Measurement.Motion.Speeds;

namespace Mikita.Measurement.Motion.Velocities2D;

public interface IVelocity2D<out T>
	{
		ISpeed<T> X { get; }
		
		ISpeed<T> Y { get; }
	}