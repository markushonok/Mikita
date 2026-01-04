using Mikita.Observation.Events;
using System;

namespace Mikita.Observation.Change;

public sealed class Observed<T>
	(
		Func<T> current,
		IEvent<Action> changed
	)
	: IObserved<T>
	{
		public T Current
			=> current();

		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}
	}