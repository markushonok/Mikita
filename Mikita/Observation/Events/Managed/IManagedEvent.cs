using System;

namespace Mikita.Observation.Events.Managed;

public interface IManagedEvent<T>: Event<T>
	{
		void Raise(Action<T> arouse);
	}