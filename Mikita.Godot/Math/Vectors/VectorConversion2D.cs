using Godot;
using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Planar;

namespace Mikita.Godot.Math.Vectors;

public static class VectorConversion2D
	{
		public static Vector2D<float> ToMikita
			(
				this Vector2 vector
			)
			=> Vector.PointingTo(vector.X, vector.Y);
	}