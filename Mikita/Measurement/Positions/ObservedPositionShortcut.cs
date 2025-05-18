using Mikita.Measurement.Lengths;
using Mikita.Observation.Change;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static class ObservedPositionShortcut
	{
		public static ILength<T> X<T>
			(
				this Observed<IPosition3D<T>> position
			)
			where T: INumber<T>
			=> position.Current.X;

		public static ILength<T> Y<T>
			(
				this Observed<IPosition3D<T>> position
			)
			where T: INumber<T>
			=> position.Current.Y;

		public static ILength<T> Z<T>
			(
				this Observed<IPosition3D<T>> position
			)
			where T: INumber<T>
			=> position.Current.Z;
	}