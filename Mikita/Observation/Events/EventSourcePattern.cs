using Mikita.Observation.Events.Raising;
using System;

namespace Mikita.Observation.Events;

public sealed class EventSourcePattern<T>
	(
		Action<T> add,
		Action<T> remove,
		EventRaise<T> raise
	)
	: IEventSource<T>
	{
		public void Add(T reaction)
			=> add(reaction);

		public void Remove(T reaction)
			=> remove(reaction);

		public void Raise(Action<T> pattern)
			=> raise(pattern);
	}