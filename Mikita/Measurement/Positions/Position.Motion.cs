using Mikita.Measurement.Motion;
using Mikita.Measurement.Motion.Velocities3D;
using Mikita.Observation.Change;
using Mikita.Structs.Referring;
using System;
using System.Numerics;

namespace Mikita.Measurement.Positions;

public static partial class Position
	{
		public static void MoveWith<T>
			(
				this IShown<IPosition3D<T>> position,
				IVelocity3D<T> velocity,
				TimeSpan duration
			)
			where T: INumber<T>, IRootFunctions<T>
			=> position.AsRef.MoveWith(velocity, duration);

		public static void MoveWith<T>
			(
				this IRef<IPosition3D<T>> position,
				IVelocity3D<T> velocity,
				TimeSpan duration
			)
			where T: INumber<T>, IRootFunctions<T>
			=> position.Value += velocity * duration;
	}