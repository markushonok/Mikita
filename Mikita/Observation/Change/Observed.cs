using Mikita.Observation.Events;
using System;

namespace Mikita.Observation.Change;

public sealed class Observed<T>
	(
		Event<Action> changed,
		Func<T> current
	)
	: IObserved<T>
	{
		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}

		public T Current
			=> current();
	}