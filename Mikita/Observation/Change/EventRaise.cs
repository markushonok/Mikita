using Mikita.Observation.Events.Sources;
using System;

namespace Mikita.Observation.Change;

public static class EventRaise
	{
		public static void Raise
			(
				this IEventSource<Action> @event
			)
			=> @event.Listener();
	}