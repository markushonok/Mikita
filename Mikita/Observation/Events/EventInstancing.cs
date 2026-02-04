using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events;

public static class EventInstancing
	{
		extension<T>(Event<T>)
			{
				public static Event<T> NewEmpty
					=> Event.With<T>([]);
			}

		extension(Event)
			{
				public static EventPattern<T> Pattern<T>
					(
						Action<T> add,
						Action<T> remove
					)
					=> new(add, remove);

				public static Event<Action> NewEmpty
					=> new([]);

				public static Event<T> With<T>
					(
						ICollection<T> reactions
					)
					=> new(reactions);
			}
	}