using Mikita.Math.Vectors;
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
			where T: INumber<T>
			=> new(x, y);

		public static Position3D<T> InMetersPointedBy<T>
			(
				IVector3D<T> vector
			)
			where T: INumber<T>, IRootFunctions<T>
			=> new
				(
					Length.FromMeters(vector.X),
					Length.FromMeters(vector.Y),
					Length.FromMeters(vector.Z)
				);
		
		public static Position3D<T> At<T>
			(
				ILength<T> x, 
				ILength<T> y,
				ILength<T> z
			)
			where T: INumber<T>
			=> new(x, y, z);
	}