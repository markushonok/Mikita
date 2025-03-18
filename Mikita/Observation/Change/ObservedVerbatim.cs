using Mikita.Observation.Events;
using System;

namespace Mikita.Observation.Change;

public class ObservedVerbatim<T>
	(
		Event<Action> changed,
		Func<T> current
	)
	: Observed<T>
	{
		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}

		public T Current
			=> current();
	}