using Mikita.Math.Numbers;
using System.Numerics;

namespace Mikita.Math.Vectors;

public static class VectorStepping3D
	{
		public static Vector3D<T> NormalStepTo<T>
			(
				this Vector3D<T> from,
				Vector3D<T> to,
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
		
		public static VectorRecord3D<T> StepTo<T>
			(
				this Vector3D<T> from,
				Vector3D<T> to,
				Vector3D<T> by
			)
			where T: 
				INumber<T>, 
				IRootFunctions<T>,
				IFloatingPointConstants<T>
			{
				return Vector.PointingTo
					(
						x: from.X.StepTo(to.X, by.X),
						y: from.Y.StepTo(to.Y, by.Y),
						z: from.Z.StepTo(to.Z, by.Z)
					);
			}
	}