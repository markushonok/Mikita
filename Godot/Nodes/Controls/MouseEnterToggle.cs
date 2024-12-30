using Godot;
using Mikita.Observing;

namespace Mikita.Godot.Nodes.Controls;

public static class MouseEnterToggle
	{
		public static Func<bool> IsMouseInsideFunc
			(
				this Control control
			)
			=> EventToggle.From
				(
					reaction => control.MouseEntered += reaction,
					reaction => control.MouseExited -= reaction
				);
	}