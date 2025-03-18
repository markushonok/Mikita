using Mikita.Measurement.Positions;
using System;

namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<out T>
	{
		static VelocityRecord3D<T> operator +
			(
				Velocity3D<T> augend, 
				Velocity3D<T> addend
			)
			=> new
				(
					augend.X + addend.X, 
					augend.Y + addend.Y, 
					augend.Z + addend.Z
				);
		
		static VelocityRecord3D<T> operator -
			(
				Velocity3D<T> minuend, 
				Velocity3D<T> subtrahend
			)
			=> new
				(
					minuend.X - subtrahend.X, 
					minuend.Y - subtrahend.Y, 
					minuend.Z - subtrahend.Z
				);

		static Velocity3D<T> operator *
			(
				Velocity3D<T> multiplicand,
				T multiplier
			)
			=> Velocity.Of
				(
					multiplicand.X * multiplier, 
					multiplicand.Y * multiplier,
					multiplicand.Z * multiplier
				);

		public static Position3D<T> operator *
			(
				Velocity3D<T> velocity,
				TimeSpan duration
			)
			=> Position.At
				(
					velocity.X * duration,
					velocity.Y * duration,
					velocity.Z * duration
				);
		
		static Velocity3D<T> operator /
			(
				Velocity3D<T> dividend, 
				T divisor
			)
			=> Velocity.Of
				(
					dividend.X / divisor,
					dividend.Y / divisor,
					dividend.Z / divisor
				);
	}