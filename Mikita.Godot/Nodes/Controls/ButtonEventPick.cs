using Godot;
using Mikita.Observation.Events;
using System;

namespace Mikita.Godot.Nodes.Controls;

public static class ButtonEventPick
	{
		extension(Button button)
			{
				public IEvent<Action> PressedEvent
					=> Event.Pattern<Action>
						(
							add: x => button.Pressed += x,
							remove: x => button.Pressed -= x
						);
			}
	}