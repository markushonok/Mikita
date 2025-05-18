namespace Mikita.Math.Vectors;

partial interface IVector3D<out T>
	{
		static IVector3D<T> operator +
			(
				IVector3D<T> augend,
				IVector3D<T> addend
			)
			=> Vector.PointingTo
				(
					augend.X + addend.X,
					augend.Y + addend.Y,
					augend.Z + addend.Z
				);
    
		static IVector3D<T> operator -
			(
				IVector3D<T> minuend,
				IVector3D<T> subtrahend
			)
			=> Vector.PointingTo
				(
					minuend.X - subtrahend.X,
					minuend.Y - subtrahend.Y,
					minuend.Z - subtrahend.Z
				);

		static IVector3D<T> operator *
			(
				IVector3D<T> multiplicand,
				T multiplier
			)
			=> multiplicand * Vector.PointingTo
				(
					x: multiplier,
					y: multiplier,
					z: multiplier
				);
    
		static IVector3D<T> operator *
			(
				IVector3D<T> multiplicand,
				IVector3D<T> multiplier
			)
			=> Vector.PointingTo
				(
					multiplicand.X * multiplier.X, 
					multiplicand.Y * multiplier.Y,
					multiplicand.Z * multiplier.Z
				);
    
		static IVector3D<T> operator /
			(
				IVector3D<T> dividend,
				T divisor
			)
			=> dividend / Vector.PointingTo(x: divisor, y: divisor, z: divisor);
    
		static IVector3D<T> operator /
			(
				IVector3D<T> dividend,
				IVector3D<T> divisor
			)
			=> Vector.PointingTo
				(
					dividend.X / divisor.X, 
					dividend.Y / divisor.Y,
					dividend.Z / divisor.Z
				);
	}