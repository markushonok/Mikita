namespace Mikita.Measurement.Lengths;

partial struct ValueLength<T>
	{
		public static bool operator ==
			(
				ValueLength<T> left,
				ILength<T> right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				ValueLength<T> left,
				ILength<T> right
			)
			=> !left.Equals(right);

		public override bool Equals(object? @object)
			=> @object is ILength<T> length && Equals(length);

		public bool Equals(ILength<T>? other)
			=> other != null && Meters.Equals(other.Meters);

		public override int GetHashCode()
			=> meters.GetHashCode();
	}