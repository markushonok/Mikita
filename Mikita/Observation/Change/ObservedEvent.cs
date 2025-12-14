using Mikita.Observation.Events;
using System;

namespace Mikita.Observation.Change;

public static class ObservedEvent
	{
		public static IEvent<Action> ChangedEvent
			(
				this IObserved<object> observed
			)
			=> Event.Pattern<Action>
				(
					add: x => observed.Changed += x,
					remove: x => observed.Changed -= x
				);
	}