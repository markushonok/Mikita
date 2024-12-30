using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public static class Size
	{
		public static SizeRecord2D<T> Of<T>
			(
				Length<T> width,
				Length<T> height
			) 
			where T : INumber<T>
			=> new(width, height);
		
		public static SizeRecord3D<T> Of<T>
			(
				Length<T> width,
				Length<T> height,
				Length<T> depth 
			) 
			where T : INumber<T>
			=> new(width, height, depth);
	}