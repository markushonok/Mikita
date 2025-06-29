using System;

namespace Mikita.Measurement.Positions;

partial class Position3D<T>: IEquatable<IPosition3D<T>>
	{
		public override bool Equals(object? @object)
			=> @object is IPosition3D<T> position && Equals(position);

		public bool Equals(IPosition3D<T>? other)
			=> other != null
				&& X.Equals(other.X)
				&& Y.Equals(other.Y)
				&& Z.Equals(other.Z);

		public override int GetHashCode()
			=> HashCode.Combine(X, Y, Z);
	}