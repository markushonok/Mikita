using Mikita.Observation.Events.Managed;
using System;

namespace Mikita.Observation.Change;

public static class EventRaise
	{
		public static void Raise(this IManagedEvent<Action> @event)
			=> @event.Raise(x => x());
	}