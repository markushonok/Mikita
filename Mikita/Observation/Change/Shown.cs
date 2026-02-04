using Mikita.Observation.Events;
using Mikita.Observation.Events.Raising;
using Mikita.Structs.Referring;
using System;

namespace Mikita.Observation.Change;

public sealed class Shown<T>
	(
		IRef<T> current,
		IEventSource<Action> changed
	)
	: IShown<T>
	{
		public T Current
			{
				get => current.Value;
				set => ChangeTo(value);
			}

		private void ChangeTo(T value)
			{
				current.Value = value;
				changed.Raise();
			}

		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}
	}