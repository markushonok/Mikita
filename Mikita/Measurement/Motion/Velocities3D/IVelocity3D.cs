using Mikita.Measurement.Motion.Speeds;

namespace Mikita.Measurement.Motion.Velocities3D;

public interface IVelocity3D<out T>
	{
		ISpeed<T> X { get; }
		
		ISpeed<T> Y { get; }
		
		ISpeed<T> Z { get; }
	}