using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static class PositionCaching
	{
		public static ValuePosition3D<T> Snapshot<T>
			(
				this IPosition3D<T> position
			)
			where T: struct, INumber<T>
			=> new
				(
					position.X.Snapshot,
					position.Y.Snapshot,
					position.Z.Snapshot
				);
	}