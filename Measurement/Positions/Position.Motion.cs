using Mikita.Many.Scalars;
using Mikita.Measurement.Lengths;
using Mikita.Measurement.Motion;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static partial class Position
	{
		public static void MoveWith<T>
			(
				this Scalar<Position3D<T>> position,
				Velocity3D<T> velocity
			)
			where T: INumber<T>, IRootFunctions<T>
			=> position.Value = position.Value.MovedWith(velocity);
		
		public static Position3D<T> MovedWith<T>
			(
				this Position3D<T> position,
				Velocity3D<T> velocity
			)
			where T: INumber<T>, IRootFunctions<T>
			=> Position.At
				(
					position.X + Length.InMeters(velocity.X.InMetersPerSecond),
					position.Y + Length.InMeters(velocity.Y.InMetersPerSecond),
					position.Z + Length.InMeters(velocity.Z.InMetersPerSecond)
				);
	}