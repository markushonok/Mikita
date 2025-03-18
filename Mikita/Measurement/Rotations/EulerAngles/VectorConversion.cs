using Mikita.Math.Vectors;
using Mikita.Measurement.Angles;
using System.Numerics;
using Vector = Mikita.Math.Vectors.Vector;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class VectorConversion
	{
		public static Vector2D<T> ToRadianVector<T>
			
			(this IEulerAngle<T> rotation)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Vector.PointingTo
				(
					rotation.Horizontal.InRadians, 
					rotation.Vertical.InRadians
				);
		
		public static IEulerAngle<T> ToRotationInRadians<T>
			
			(this Vector2D<T> vector) 
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Rotation.Of
				(
					horizontal: Angle.Radians(vector.X),
					vertical: Angle.Radians(vector.Y)
				);
	}