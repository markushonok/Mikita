using Mikita.Graphics.Colors;

namespace Mikita.Godot.Graphics.Colors;

using GodotColor = global::Godot.Color;

public static class ColorConversion
	{
		public static GodotColor ToGodot(Color<float> color)
			=> new
				(
					color.Red,
					color.Green,
					color.Blue,
					color.Alpha
				);
	}