using Godot;

namespace Mikita.Godot.Nodes.Controls;

public static class ControlPositioning
	{
		public static Vector2 GlobalCenterPosition
			(
				this Control control
			)
			=> control.GlobalPosition + control.Size / 2;
		
		public static Vector2 TopLeftCornerOffset
			(
				this Control control
			)
			=> -control.Size / 2;
		
		public static Vector2 TopRightCornerOffset
			(
				this Control control
			)
			=> new(control.Size.X / 2, -control.Size.Y / 2);

		public static Vector2 BottomRightCornerOffset
			(
				this Control control
			)
			=> control.Size / 2;
		
		public static Vector2 BottomLeftCornerOffset
			(
				this Control control
			)
			=> new(-control.Size.X / 2, control.Size.Y / 2);
	}