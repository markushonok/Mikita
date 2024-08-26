using Mikita.Subroutine.Observing;

namespace Mikita.Subroutine.Properties.Observing;

public interface ObservedProperty<T> 
	: 
		Property<T>, 
		ObservedChange;