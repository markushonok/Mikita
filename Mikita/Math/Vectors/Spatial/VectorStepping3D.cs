using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Math.Vectors.Spatial;

public static class VectorStepping3D
	{
		public static IVector3D<T> NormalStepTo<T>
			(
				this IVector3D<T> from,
				IVector3D<T> to,
				T by
			)
			where T : INumber<T>, IRootFunctions<T>
			{
				var local = to - from;
				var length = local.Length;
				if (length == T.Zero) return to;
				var normal = local / length;
				return from.StepTo(to, normal * by);
			}
		
		public static Vector3D<T> StepTo<T>
			(
				this IVector3D<T> from,
				IVector3D<T> to,
				IVector3D<T> by
			)
			where T: 
				INumber<T>, 
				IRootFunctions<T>,
				IFloatingPointConstants<T>
			{
				return Vector.PointingTo
					(
						x: from.X.Stepped(to.X, by.X),
						y: from.Y.Stepped(to.Y, by.Y),
						z: from.Z.Stepped(to.Z, by.Z)
					);
			}
	}