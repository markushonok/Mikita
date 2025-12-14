namespace Mikita.Observation.Events;

public interface IEvent<in T>
	{
		void Add(T reaction);

		void Remove(T reaction);
	}