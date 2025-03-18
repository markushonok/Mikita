using Mikita.Measurement.Motion;
using Mikita.Observation.Change;
using Mikita.Structs.Scalars;
using System;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static partial class Position
	{
		public static void MoveWith<T>
			(
				this Interserved<Position3D<T>> position,
				Velocity3D<T> velocity,
				TimeSpan duration
			)
			where T: INumber<T>, IRootFunctions<T>
			=> position.AsScalar.MoveWith(velocity, duration);

		public static void MoveWith<T>
			(
				this Scalar<Position3D<T>> position,
				Velocity3D<T> velocity,
				TimeSpan duration
			)
			where T: INumber<T>, IRootFunctions<T>
			=> position.Value += velocity * duration;
	}