using Mikita.Subroutine.Properties.Observing;
using System.Numerics;

namespace Mikita.Aliases;

public interface ObservedPropertyAlias<TOrigin, out TNew>
	: 
		ObservedProperty<TOrigin>, 
		Alias<ObservedProperty<TOrigin>, TNew>

	where TNew: 
		ObservedProperty<TOrigin>,
		Alias<ObservedProperty<TOrigin>, TNew>

	{
		static TNew Of(TOrigin origin)
			=> Of(property: ObservedRef.To(origin));
		
		new static TNew Of(ObservedProperty<TOrigin> property) 
			=> Of(ref property);
	}