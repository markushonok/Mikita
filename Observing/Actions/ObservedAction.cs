using Mikita.Aliases;
using Mikita.Observing.Events;

namespace Mikita.Observing.Actions;

public interface ObservedAction<out T> 
	: PrePostEvent
	{
		T Invoke { get; }
	}
	
public partial interface ObservedAction 

	: ObservedAction<Action>
	, Alias
		<
			ObservedAction<Action>, 
			ObservedAction
		>;