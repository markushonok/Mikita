using Godot;
using Mikita.Math.Sizes;
using Mikita.Math.Vectors.Planar;

namespace Mikita.Godot.Math.Sizes;

public static class SizeConversion2D
	{
		public static Vector2 ToGodot
			(
				this ISize2D<float> size
			)
			=> new(size.X, size.Y);

		public static Vector2I ToGodot
			(
				this ISize2D<int> size
			)
			=> new(size.X, size.Y);

		extension(Vector2 vector)
			{
				public Size2D<int> ToMikitaIntSize()
					=> Size.Of(vector.X, vector.Y).ToInt;

				public Size2D<float> ToMikitaSize()
					=> Size.Of(vector.X, vector.Y);
			}

	}