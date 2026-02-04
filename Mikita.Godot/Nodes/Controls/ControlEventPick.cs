using Godot;
using Mikita.Observation.Events;
using Mikita.Structs.Referring;
using GuiInputReaction = Godot.Control.GuiInputEventHandler;

namespace Mikita.Godot.Nodes.Controls;

public static class ControlEventPick
	{
		extension(Control node)
			{
				public IEvent<Action> ResizedEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Resized += x,
							remove: x => node.Resized -= x
						);

				public IEvent<GuiInputReaction> GuiInputEvent
					=> Event.Pattern<GuiInputReaction>
						(
							add: x => node.GuiInput += x,
							remove: x => node.GuiInput -= x
						);

				public IEvent<Action> HoveredOnEvent
					=> Event.Pattern<Action>
						(
							add: x => node.MouseEntered += x,
							remove: x => node.MouseEntered -= x
						);

				public IEvent<Action> HoveredOutEvent
					=> Event.Pattern<Action>
						(
							add: x => node.MouseExited += x,
							remove: x => node.MouseExited -= x
						);

				public IEvent<Action> FocusGrabbedEvent
					=> Event.Pattern<Action>
						(
							add: x => node.FocusEntered += x,
							remove: x => node.FocusEntered -= x
						);

				public IEvent<Action> FocusLostEvent
					=> Event.Pattern<Action>
						(
							add: x => node.FocusExited += x,
							remove: x => node.FocusExited -= x
						);
			}

		extension(IRef<Control> node)
			{
				public IEvent<Action> ResizedEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Value.Resized += x,
							remove: x => node.Value.Resized -= x
						);

				public IEvent<GuiInputReaction> GuiInputEvent
					=> Event.Pattern<GuiInputReaction>
						(
							add: x => node.Value.GuiInput += x,
							remove: x => node.Value.GuiInput -= x
						);

				public IEvent<Action> HoveredOnEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Value.MouseEntered += x,
							remove: x => node.Value.MouseEntered -= x
						);

				public IEvent<Action> HoveredOutEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Value.MouseExited += x,
							remove: x => node.Value.MouseExited -= x
						);

				public IEvent<Action> FocusGrabbedEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Value.FocusEntered += x,
							remove: x => node.Value.FocusEntered -= x
						);

				public IEvent<Action> FocusLostEvent
					=> Event.Pattern<Action>
						(
							add: x => node.Value.FocusExited += x,
							remove: x => node.Value.FocusExited -= x
						);
			}
	}