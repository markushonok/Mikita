namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record EulerAngle<T>
	{
		public static IEulerAngle<T> operator +
			(
				EulerAngle<T> augend,
				IEulerAngle<T> addend
			)
			=> (IEulerAngle<T>) augend + addend;
		
		public static IEulerAngle<T> operator -
			(
				EulerAngle<T> minuend,
				IEulerAngle<T> subtrahend
			)
			=> (IEulerAngle<T>) minuend - subtrahend;

		public static IEulerAngle<T> operator *
			(
				EulerAngle<T> multiplicand,
				T multiplier
			)
			=> (IEulerAngle<T>) multiplicand * multiplier;
		
		public static IEulerAngle<T> operator *
			(
				EulerAngle<T> multiplicand,
				IEulerAngle<T> multiplier
			)
			=> (IEulerAngle<T>) multiplicand * multiplier;
		
		public static IEulerAngle<T> operator /
			(
				EulerAngle<T> dividend,
				T divisor
			)
			=> (IEulerAngle<T>) dividend / divisor;
		
		public static IEulerAngle<T> operator /
			(
				EulerAngle<T> dividend,
				IEulerAngle<T> divisor
			)
			=> (IEulerAngle<T>) dividend / divisor;
	}