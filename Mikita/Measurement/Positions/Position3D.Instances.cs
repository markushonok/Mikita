using Mikita.Measurement.Lengths;

namespace Mikita.Measurement.Positions;

sealed partial class Position3D<T>
	{
		public static Position3D<T> Zero
			=> new
				(
					Length<T>.Zero,
					Length<T>.Zero,
					Length<T>.Zero
				);
	}