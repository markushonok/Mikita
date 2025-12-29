namespace Mikita.Measurement.Lengths;

partial class Length<T>
	{
		public static bool operator ==
			(
				Length<T> left,
				ILength<T> right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				Length<T> left,
				ILength<T> right
			)
			=> !left.Equals(right);

		public override bool Equals(object? @object)
			=> ReferenceEquals(this, @object)
				|| @object is ILength<T> length && Equals(length);

		public bool Equals(ILength<T>? other)
			=> other != null && Meters.Equals(other.Meters);

		public override int GetHashCode()
			=> meters.GetHashCode();
	}