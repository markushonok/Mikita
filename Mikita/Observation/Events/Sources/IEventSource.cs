namespace Mikita.Observation.Events.Sources;

public interface IEventSource<T>: IEvent<T>
	{
		T Listener { get; }
	}