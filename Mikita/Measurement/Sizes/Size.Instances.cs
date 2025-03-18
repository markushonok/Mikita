using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public static class Size
	{
		public static Size2D<T> Of<T>
			(
				ILength<T> width,
				ILength<T> height
			) 
			where T : INumber<T>
			=> new(width, height);
		
		public static Size3D<T> Of<T>
			(
				ILength<T> width,
				ILength<T> height,
				ILength<T> depth 
			) 
			where T : INumber<T>
			=> new(width, height, depth);
	}