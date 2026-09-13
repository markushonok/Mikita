using Mikita.Logging;
using Mikita.Observation.Events.Raising;
using System;
using System.Collections.Generic;
using System.Threading;

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

				public static EventSourcePattern<T> Pattern<T>
					(
						Action<T> add,
						Action<T> remove,
						EventRaise<T> raise
					)
					=> new(add, remove, raise);

				public static Event<Action> NewEmpty
					=> new([]);

				public static Event<T> With<T>
					(
						ICollection<T> reactions
					)
					=> new(reactions);
			}

		extension<T>(IEventSource<T> source)
			{
				/// <summary>
				/// Adds serialized access. The source must no longer be accessed
				/// directly.
				/// </summary>
				public IEventSource<T> WithSerialAccess
					=> new SerialAccessEvent<T>
						(
							source,
							new Lock()
						);

				/// <summary>
				/// Logs a failed reaction and continues raising the event.
				/// </summary>
				public IEventSource<T> WithSafeRaise
					(
						ILog log
					)
					=> new RaiseSafeEvent<T>(source, log);
			}
	}