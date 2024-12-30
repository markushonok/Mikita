namespace Mikita.Observing.Unconferences;

public interface Unconference<out T>
	{
		event Action Happened;

		T Invoke { get; }
	}