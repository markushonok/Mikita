using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthCaching
	{
		public static ValueLength<T> Snapshot<T>
			(
				this ILength<T> length
			)
			where T: struct, INumber<T>
			=> new(length.Meters());
	}