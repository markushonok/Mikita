namespace Mikita.Observation.Events;

public partial interface Event<in T>
	{
		void Add(T reaction);

		void Remove(T reaction);
	}