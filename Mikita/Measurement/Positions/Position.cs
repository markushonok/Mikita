using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static partial class Position
	{
		public static PositionRecord2D<T> At<T>
			(
				ILength<T> x, 
				ILength<T> y
			)
			where T : INumber<T>
			=> new(x, y);
		
		public static PositionRecord3D<T> At<T>
			(
				ILength<T> x, 
				ILength<T> y,
				ILength<T> z
			)
			where T : INumber<T>
			=> new(x, y, z);
	}