using Mikita.Observing.Events;

namespace Mikita.Observing;

public interface Observed<out T> 
	: PrePostChangeEvent
	{
		T Value { get; }
	}