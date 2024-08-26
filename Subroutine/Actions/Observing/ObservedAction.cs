using Mikita.Subroutine.Observing;

namespace Mikita.Subroutine.Actions.Observing;

public interface ObservedAction<out T> : PrePostEvent
	{
		T Invoke { get; }
	}