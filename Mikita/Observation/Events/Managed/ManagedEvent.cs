namespace Mikita.Observation.Events.Managed;

public interface ManagedEvent<T>: Event<T>
	{
		T Invoke { get; }
	}