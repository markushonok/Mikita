using System;

namespace Mikita.Math.Vectors.Spatial;

partial class Vector3D<T>: IEquatable<IVector3D<T>>
	{
		public override bool Equals
			(
				object? @object
			)
			=> ReferenceEquals(this, @object)
				|| @object is IVector3D<T> other && Equals(other);

		public bool Equals
			(
				IVector3D<T>? other
			)
			=> other is not null
				&& Equals(X, other.X)
				&& Equals(Y, other.Y)
				&& Equals(Z, other.Z);

		public override int GetHashCode()
			=> HashCode.Combine(X, Y, Z);
	}