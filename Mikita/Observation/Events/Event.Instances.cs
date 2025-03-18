using System;

namespace Mikita.Observation.Events;

public static class Event
	{
		public static EventVerbatim<T> WithPattern<T>
			(
				Action<T> add,
				Action<T> remove
			)
			=> new(add, remove);

	}