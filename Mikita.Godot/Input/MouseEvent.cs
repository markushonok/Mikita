using Godot;

namespace Mikita.Godot.Input;

public static class MouseEvent
	{
		public static bool IsPressedLmbEvent
			(
				this InputEvent @event
			)
			=> @event is InputEventMouseButton
				{
					ButtonIndex: MouseButton.Left,
					Pressed: true
				};

		public static bool IsReleasedLmbEvent
			(
				this InputEvent @event
			)
			=> @event is InputEventMouseButton
				{
					ButtonIndex: MouseButton.Left,
					Pressed: false
				};
	}