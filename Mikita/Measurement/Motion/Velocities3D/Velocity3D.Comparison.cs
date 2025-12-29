using System;

namespace Mikita.Measurement.Motion.Velocities3D;

partial class Velocity3D<T>: IEquatable<IVelocity3D<T>>
	{
		public override bool Equals
			(
				object? @object
			)
			=> ReferenceEquals(this, @object)
				|| @object is Velocity3D<T> other && Equals(other);

		public bool Equals
			(
				IVelocity3D<T>? other
			)
			=> other is not null
				&& Equals(x, other.X)
				&& Equals(y, other.Y)
				&& Equals(z, other.Z);

		public override int GetHashCode()
			=> HashCode.Combine(x, y, z);
	}