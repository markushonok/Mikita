namespace Mikita.Observation.Events;

public interface Event<in T>
	{
		void Add(T reaction);

		void Remove(T reaction);
	}