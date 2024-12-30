using Mikita.Math.Vectors;
using Mikita.Measurement.Angles;
using System.Numerics;
using Vector = Mikita.Math.Vectors.Vector;

namespace Mikita.Measurement.Rotations;

public static class VectorConversion
	{
		public static Vector2D<T> ToRadianVector<T>
			
			(this Rotation2D<T> rotation) 
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Vector.PointingTo
				(
					rotation.Horizontal.InRadians, 
					rotation.Vertical.InRadians
				);
		
		public static Rotation2D<T> ToRotationInRadians<T>
			
			(this Vector2D<T> vector) 
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Rotation.Of
				(
					horizontal: vector.X.AsAngleInRadians(),
					vertical: vector.Y.AsAngleInRadians()
				);
	}