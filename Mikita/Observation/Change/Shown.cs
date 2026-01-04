using Mikita.Observation.Events;
using Mikita.Structs.Referring;
using System;

namespace Mikita.Observation.Change;

public sealed class Shown<T>
	(
		IRef<T> current,
		IEvent<Action> changed
	)
	: IShown<T>
	{
		public T Current
			{
				get => current.Value;
				set => current.Value = value;
			}

		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}
	}