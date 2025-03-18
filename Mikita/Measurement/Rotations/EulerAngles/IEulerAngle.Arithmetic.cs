namespace Mikita.Measurement.Rotations.EulerAngles;

public partial interface IEulerAngle<out T>
	{
		static IEulerAngle<T> operator +
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> Rotation.Of
				(
					left.X + right.X,
					left.Y + right.Y,
					left.Z + right.Z,
					left.Order
				);
		
		static IEulerAngle<T> operator -
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> Rotation.Of
				(
					left.X - right.X,
					left.Y - right.Y,
					left.Z - right.Z,
					left.Order
				);

		static IEulerAngle<T> operator *
			(
				IEulerAngle<T> left,
				T right
			)
			=> Rotation.Of
				(
					left.X * right,
					left.Y * right,
					left.Z * right,
					left.Order
				);
		
		static IEulerAngle<T> operator *
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> Rotation.Of
				(
					left.X * right.X,
					left.Y * right.Y,
					left.Z * right.Z,
					left.Order
				);
		
		static IEulerAngle<T> operator /
			(
				IEulerAngle<T> left,
				T right
			)
			=> Rotation.Of
				(
					left.X / right,
					left.Y / right,
					left.Z / right,
					left.Order
				);
		
		static IEulerAngle<T> operator /
			(
				IEulerAngle<T> left,
				IEulerAngle<T> right
			)
			=> Rotation.Of
				(
					left.X / right.X, 
					left.Y / right.Y,
					left.Z / right.Z,
					left.Order
				);
	}
