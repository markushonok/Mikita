namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record struct EulerAngle<T>
	{
		public static IEulerAngle<T> operator +
			(
				EulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left + right;
		
		public static IEulerAngle<T> operator -
			(
				EulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left - right;

		public static IEulerAngle<T> operator *
			(
				EulerAngle<T> left,
				T right
			)
			=> (IEulerAngle<T>) left * right;
		
		public static IEulerAngle<T> operator *
			(
				EulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left * right;
		
		public static IEulerAngle<T> operator /
			(
				EulerAngle<T> left,
				T right
			)
			=> (IEulerAngle<T>) left / right;
		
		public static IEulerAngle<T> operator /
			(
				EulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left / right;
	}