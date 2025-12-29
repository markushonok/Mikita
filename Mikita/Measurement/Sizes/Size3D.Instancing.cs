using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public static class Size3D
	{
		public static Size3D<T> Of<T>
			(
				ILength<T> width
			)
			where T : INumber<T>
			=> new(width, width, width);
	}