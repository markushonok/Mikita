namespace Mikita.Subroutine.Actions.Observing;

public interface ActionObservation<out T>
	{
		T Invoke { get; }
		
		event Action Happening;
		
		event Action Happened;
	}