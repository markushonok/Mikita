using Godot;
using Mikita.Observation;

namespace Mikita.Godot.Nodes.Controls;

public static class FocusEnterToggle
	{
		extension(Control control)
			{
				public Func<bool> IsFocused
					=> EventToggle.From
						(
							reaction => control.FocusEntered += reaction,
							reaction => control.FocusExited += reaction
						);
			}
	}