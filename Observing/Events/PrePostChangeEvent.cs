namespace Mikita.Observing.Events;

public interface PrePostChangeEvent
	{
		event Action Changing;

		event Action Changed;
	}