using System;

namespace Mikita.Observation.Events;

public class EventVerbatim<T>
	(
		Action<T> add,
		Action<T> remove
	)
	: Event<T>
	{
		public void Add(T reaction)
			=> add(reaction);

		public void Remove(T reaction)
			=> remove(reaction);
	}