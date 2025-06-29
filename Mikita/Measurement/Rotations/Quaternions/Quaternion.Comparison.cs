using System;

namespace Mikita.Measurement.Rotations.Quaternions;

partial class Quaternion<T>: IEquatable<IQuaternion<T>>
	{
		public static bool operator ==
			(
				Quaternion<T> left,
				IQuaternion<T> right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				Quaternion<T> left,
				IQuaternion<T> right
			)
			=> !left.Equals(right);

		public override bool Equals(object? obj)
			=> obj is IQuaternion<T> other && Equals(other);

		public bool Equals(IQuaternion<T>? other)
			=> other != null
				&& W.Equals(other.W)
				&& X.Equals(other.X)
				&& Y.Equals(other.Y)
				&& Z.Equals(other.Z);

		public override int GetHashCode()
			=> HashCode.Combine(W, X, Y, Z);
	}