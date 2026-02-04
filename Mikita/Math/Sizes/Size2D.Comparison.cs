using System;

namespace Mikita.Math.Sizes;

partial class Size2D<T>
	{
		public override bool Equals
			(
				object? @object
			)
			=> @object is ISize2D<T> other
				&& Equals(other);

		private bool Equals(ISize2D<T> other)
			=> x.Equals(other.X)
				&& y.Equals(other.Y);

		public override int GetHashCode()
			=> HashCode.Combine(x, y);
	}