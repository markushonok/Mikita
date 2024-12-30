namespace Mikita.Measurement.Motion;

public sealed partial record VelocityRecord3D<T>
	{
		public static VelocityRecord3D<T> operator +
			(
				VelocityRecord3D<T> augend, 
				Velocity3D<T> addend
			)
			=> (Velocity3D<T>) augend + addend;

		public static VelocityRecord3D<T> operator -
			(
				VelocityRecord3D<T> minuend,
				Velocity3D<T> subtrahend
			)
			=> (Velocity3D<T>) minuend - subtrahend;

		public static Velocity3D<T> operator *
			(
				VelocityRecord3D<T> multiplicand,
				T multiplier
			)
			=> (Velocity3D<T>) multiplicand * multiplier;
		
		public static Velocity3D<T> operator *
			(
				VelocityRecord3D<T> multiplicand, 
				Velocity3D<T> multiplier
			)
			=> (Velocity3D<T>) multiplicand * multiplier;
		
		public static Velocity3D<T> operator /
			(
				VelocityRecord3D<T> dividend, 
				T divisor
			)
			=> (Velocity3D<T>) dividend / divisor;
		
		public static Velocity3D<T> operator /
			(
				VelocityRecord3D<T> dividend, 
				Speed<T> divisor
			)
			=> (Velocity3D<T>) dividend / divisor;
		
		public static Velocity3D<T> operator /
			(
				VelocityRecord3D<T> dividend, 
				Velocity3D<T> divisor
			)
			=> (Velocity3D<T>) dividend / divisor;
	}