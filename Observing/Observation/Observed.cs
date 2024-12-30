namespace Mikita.Observing.Observation;

public interface Observed<out T>
	{
		event Action Changed;

		T Current { get; }
	}