using System;

namespace Mikita.Observation.Events;

public sealed class EventPattern<T>
	(
		Action<T> add,
		Action<T> remove
	)
	: IEvent<T>
	{
		public void Add(T reaction)
			=> add(reaction);

		public void Remove(T reaction)
			=> remove(reaction);
	}