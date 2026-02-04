using System;

namespace Mikita.Observation.Events;

public interface IEventSource<T>
	: IEvent<T>
	{
		void Raise(Action<T> pattern);
	}