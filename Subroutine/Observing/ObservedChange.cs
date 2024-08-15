namespace Mikita.Subroutine.Observing;

public interface ObservedChange
	{
		event Action Changing;
		
		event Action Changed;
	}