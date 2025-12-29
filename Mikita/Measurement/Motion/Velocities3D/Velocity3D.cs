using Mikita.Measurement.Motion.Speeds;

namespace Mikita.Measurement.Motion.Velocities3D;

public sealed partial class Velocity3D<T>
	(
		ISpeed<T> x,
		ISpeed<T> y,
		ISpeed<T> z
	) 
	: IVelocity3D<T>
	{
		public ISpeed<T> X => x;

		public ISpeed<T> Y => y;

		public ISpeed<T> Z => z;
	}