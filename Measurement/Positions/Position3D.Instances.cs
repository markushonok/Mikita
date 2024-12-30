using Mikita.Measurement.Lengths;

namespace Mikita.Measurement.Positions;

public partial interface Position3D<out T>
	{
		static PositionRecord3D<T> Zero
			=> new
				(
					Length<T>.Zero,
					Length<T>.Zero,
					Length<T>.Zero
				);
	}