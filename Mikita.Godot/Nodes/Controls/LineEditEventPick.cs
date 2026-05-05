using Godot;
using Mikita.Observation.Events;
using static Godot.LineEdit;

namespace Mikita.Godot.Nodes.Controls;

public static class LineEditEventPick
	{
		extension(LineEdit node)
			{
				public IEvent<TextChangedEventHandler> ChangedEvent
					=> Event.Pattern<TextChangedEventHandler>
						(
							add: x => node.TextChanged += x,
							remove: x => node.TextChanged -= x
						);
			}
	}