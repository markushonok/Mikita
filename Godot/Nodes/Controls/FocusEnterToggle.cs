using Godot;
using Mikita.Observation;

namespace Mikita.Godot.Nodes.Controls;

public static class FocusEnterToggle
	{
		public static Func<bool> IsFocused
			(
				this Control control
			)
			=> EventToggle.From
				(
					reaction => control.FocusEntered += reaction,
					reaction => control.FocusExited += reaction
				);
	}