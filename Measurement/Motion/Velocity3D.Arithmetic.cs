namespace Mikita.Measurement.Motion;

public partial interface Velocity3D<T>
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
		
		static Velocity3D<T> operator *
			(
				Velocity3D<T> multiplicand,
				Speed<T> multiplier
			)
			=> Velocity.Of
				(
					multiplicand.X * multiplier, 
					multiplicand.Y * multiplier,
					multiplicand.Z * multiplier
				);
		
		static Velocity3D<T> operator *
			(
				Velocity3D<T> multiplicand, 
				Velocity3D<T> multiplier
			)
			=> Velocity.Of
				(
					multiplicand.X * multiplier.X, 
					multiplicand.Y * multiplier.Y,
					multiplicand.Z * multiplier.Z
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
		
		static Velocity3D<T> operator /
			(
				Velocity3D<T> dividend, 
				Speed<T> divisor
			)
			=> Velocity.Of
				(
					dividend.X / divisor,
					dividend.Y / divisor,
					dividend.Z / divisor
				);
		
		static Velocity3D<T> operator /
			(
				Velocity3D<T> dividend, 
				Velocity3D<T> divisor
			)
			=> Velocity.Of
				(
					dividend.X / divisor.X,
					dividend.Y / divisor.Y,
					dividend.Z / divisor.Z
				);
	}