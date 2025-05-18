namespace Mikita.Math.Vectors;

sealed partial record Vector3D<T>
	{
		public static IVector3D<T> operator +
			(
				Vector3D<T> addend,
				IVector3D<T> augend
			)
			=> (IVector3D<T>) addend + augend;

		public static IVector3D<T> operator -
			(
				Vector3D<T> minuend,
				IVector3D<T> subtrahend
			)
			=> (IVector3D<T>) minuend - subtrahend;

		public static IVector3D<T> operator *
			(
				Vector3D<T> multiplicand,
				T multiplier
			)
			=> (IVector3D<T>) multiplicand * multiplier;

		public static IVector3D<T> operator *
			(
				Vector3D<T> multiplicand,
				IVector3D<T> multiplier
			)
			=> (IVector3D<T>) multiplicand * multiplier;

		public static IVector3D<T> operator /
			(
				Vector3D<T> dividend,
				T divisor
			)
			=> (IVector3D<T>) dividend / divisor;

		public static IVector3D<T> operator /
			(
				Vector3D<T> dividend,
				IVector3D<T> divisor
			)
			=> (IVector3D<T>) dividend / divisor;
	}