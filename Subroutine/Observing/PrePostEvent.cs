namespace Mikita.Subroutine.Observing;

public interface PrePostEvent
	{
		event Action Happening;
		
		event Action Happened;
	}