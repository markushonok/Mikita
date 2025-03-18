namespace Mikita.Math.Vectors;

public partial interface Vector3D<out T>
	{
		static Vector3D<T> operator +
			(
				Vector3D<T> left,
				Vector3D<T> right
			)
			=> Vector.PointingTo
				(
					left.X + right.X, 
					left.Y + right.Y,
					left.Z + right.Z
				);
		
		static Vector3D<T> operator -
			(
				Vector3D<T> left,
				Vector3D<T> right
			)
			=> Vector.PointingTo
				(
					left.X - right.X, 
					left.Y - right.Y,
					left.Z - right.Z
				);

		static Vector3D<T> operator *
			(
				Vector3D<T> vector,
				T scalar
			)
			=> vector * Vector.PointingTo(x: scalar, y: scalar, z: scalar);
		
		static Vector3D<T> operator *
			(
				Vector3D<T> left,
				Vector3D<T> right
			)
			=> Vector.PointingTo
				(
					left.X * right.X, 
					left.Y * right.Y,
					left.Z * right.Z
				);
		
		static Vector3D<T> operator /
			(
				Vector3D<T> vector,
				T scalar
			)
			=> vector / Vector.PointingTo(x: scalar, y: scalar, z: scalar);
		
		static Vector3D<T> operator /
			(
				Vector3D<T> left,
				Vector3D<T> right
			)
			=> Vector.PointingTo
				(
					left.X / right.X, 
					left.Y / right.Y,
					left.Z / right.Z
				);
	}