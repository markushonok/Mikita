using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

partial record Size2D<T>
	{
		public static Size2D<T> Zero
			=> new
				(
					Width: Length<T>.Zero,
					Height: Length<T>.Zero
				);
	}

public static class Size2D
	{
		public static Size2D<T> Of<T>(ILength<T> length)
			where T : INumber<T>
			=> new
				(
					Width: length,
					Height: length
				);
	}