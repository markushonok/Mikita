using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using Mikita.Measurement.Lengths;
using Mikita.Measurement.Motion.Velocities2D;
using Mikita.Measurement.Motion.Velocities3D;
using System;
using System.Numerics;

namespace Mikita.Measurement.Motion.Speeds;

public static class SpeedArithmetics
	{
		extension<T>(ISpeed<T>)
			where T: INumber<T>, IRootFunctions<T>
			{
				public static ISpeed<T> operator +
					(
						ISpeed<T> augend,
						ISpeed<T> addend
					)
					=> Speed.OfMetersPerSecond
						(
							augend.MetersPerSecond
							+ addend.MetersPerSecond
						);

				public static ISpeed<T> operator -
					(
						ISpeed<T> minuend,
						ISpeed<T> subtrahend
					)
					=> Speed.OfMetersPerSecond
						(
							minuend.MetersPerSecond
							- subtrahend.MetersPerSecond
						);

				public static Velocity3D<T> operator *
					(
						ISpeed<T> multiplicand,
						IVector3D<T> multiplier
					)
					=> Velocity.Of
						(
							x: multiplicand * multiplier.X,
							y: multiplicand * multiplier.Y,
							z: multiplicand * multiplier.Z
						);

				public static ISpeed<T> operator *
					(
						ISpeed<T> multiplicand,
						T multiplier
					)
					=> Speed.OfMetersPerSecond
						(
							multiplicand.MetersPerSecond
							* multiplier
						);

				public static ISpeed<T> operator /
					(
						ISpeed<T> dividend,
						T divisor
					)
					=> Speed.OfMetersPerSecond
						(
							dividend.MetersPerSecond
							/ divisor
						);

				public static Length<T> operator *
					(
						ISpeed<T> speed,
						TimeSpan time
					)
					=> Length.FromMeters
						(
							speed.MetersPerSecond
							* T.CreateChecked(time.TotalSeconds)
						);
			}
	}