using Godot;
using Mikita.Math.Sizes;

namespace Mikita.Godot.Math.Sizes;

public static class SizeConversion2D
	{
		public static Vector2I ToGodot
			(
				this ISize2D<int> size
			)
			=> new(size.X, size.Y);
	}