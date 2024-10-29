namespace Mikita.Observing.Events;

public interface PrePostEvent
	{
		event Action Happening;

		event Action Happened;
	}