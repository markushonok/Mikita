using Godot;

namespace Mikita.Godot.Nodes.Controls;

public static class ControlPositioning
	{
		extension(Control control)
			{
				public Vector2 GlobalCenterPosition
					=> control.GlobalPosition + control.Size / 2;

				public Vector2 TopLeftCornerOffset
					=> -control.Size / 2;

				public Vector2 TopRightCornerOffset
					=> new(control.Size.X / 2, -control.Size.Y / 2);

				public Vector2 BottomRightCornerOffset
					=> control.Size / 2;

				public Vector2 BottomLeftCornerOffset
					=> new(-control.Size.X / 2, control.Size.Y / 2);
			}

	}