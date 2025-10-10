using Godot;

namespace Mikita.Godot.Nodes.Controls;

public static class MousePositioning
	{
		public static Vector2 MousePositionFromCenter
			(
				this Control control
			)
			=> control.GetGlobalMousePosition() - control.GlobalCenterPosition();
	}