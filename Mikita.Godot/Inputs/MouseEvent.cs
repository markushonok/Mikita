using Godot;

namespace Mikita.Godot.Inputs;

public static class MouseEvent
	{
		extension(InputEvent @event)
			{
				public bool IsPressedUIAcceptEvent
					=> @event.IsPressedLmbEvent
						|| @event.IsActionPressed("ui_accept");

				public bool IsReleasedUIAcceptEvent
					=> @event.IsReleasedLmbEvent
						|| @event.IsActionReleased("ui_accept");

				public bool IsPressedLmbEvent
					=> @event is InputEventMouseButton
						{
							ButtonIndex: MouseButton.Left,
							Pressed: true
						};

				public bool IsReleasedLmbEvent
					=> @event is InputEventMouseButton
						{
							ButtonIndex: MouseButton.Left,
							Pressed: false
						};
			}
	}