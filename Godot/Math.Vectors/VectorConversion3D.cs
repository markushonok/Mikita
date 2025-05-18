using Mikita.Math.Vectors;
using System.Numerics;
using Vector = Mikita.Math.Vectors.Vector;
using Vector3 = Godot.Vector3;

namespace Mikita.Godot.Math.Vectors;

public static class VectorConversion3D
	{
		public static IVector3D<float> ToMikita
			(
				this Vector3 vector
			)
			=> Vector.PointingTo(vector.X, vector.Y, vector.Z);

		public static Vector3 ToGodot<T>
			(
				this IVector3D<T> vector
			)
			where T : INumber<T>, IRootFunctions<T>
			=> new
				(
					float.CreateChecked(vector.X), 
					float.CreateChecked(vector.Y), 
					float.CreateChecked(vector.Z)
				);
	}