using Mikita.Math.Vectors;
using Mikita.Measurement.Angles;
using System.Numerics;
using Vector = Mikita.Math.Vectors.Vector;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class VectorConversion
	{
		public static IVector3D<T> ToRadianVector<T>
			
			(this IEulerAngle<T> rotation)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Vector.PointingTo
				(
					rotation.X.InRadians,
					rotation.Y.InRadians,
					rotation.Z.InRadians
				);
		
		public static IEulerAngle<T> ToRotationInRadians<T>
			
			(this IVector3D<T> vector)
		
			where T:
				struct,
				INumber<T>,
				IRootFunctions<T>
		
			=> Rotation.Of
				(
					x: Angle.Radians(vector.X),
					y: Angle.Radians(vector.Y),
					z: Angle.Radians(vector.Z)
				);
	}