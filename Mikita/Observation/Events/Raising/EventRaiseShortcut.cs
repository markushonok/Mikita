using System;

namespace Mikita.Observation.Events.Raising;

public static class EventRaiseShortcut
	{
		extension(IEventSource<Action> @event)
			{
				public void Raise()
					=> @event.Raise(x => x());
			}
	}