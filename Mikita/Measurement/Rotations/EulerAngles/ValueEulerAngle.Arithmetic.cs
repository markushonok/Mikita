namespace Mikita.Measurement.Rotations.EulerAngles;

public partial record struct ValueEulerAngle<T>
	{
		public static IEulerAngle<T> operator +
			(
				ValueEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left + right;
		
		public static IEulerAngle<T> operator -
			(
				ValueEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left - right;

		public static IEulerAngle<T> operator *
			(
				ValueEulerAngle<T> left,
				T right
			)
			=> (IEulerAngle<T>) left * right;
		
		public static IEulerAngle<T> operator *
			(
				ValueEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left * right;
		
		public static IEulerAngle<T> operator /
			(
				ValueEulerAngle<T> left,
				T right
			)
			=> (IEulerAngle<T>) left / right;
		
		public static IEulerAngle<T> operator /
			(
				ValueEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> (IEulerAngle<T>) left / right;
	}