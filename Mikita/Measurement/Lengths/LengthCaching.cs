using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthCaching
	{
		extension<T>(ILength<T> length)
			where T: struct, INumber<T>
			{
				public ValueLength<T> Snapshot
					=> new(length.Meters);
			}
	}