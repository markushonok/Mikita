using Mikita.Aliases;

namespace Mikita.Observing;

public interface ObservedSourceAlias<TOrigin, out TNew>
	
	: ObservedSource<TOrigin>
	, Alias<ObservedSource<TOrigin>, TNew>

	where TNew
		: ObservedSource<TOrigin>
		, Alias<ObservedSource<TOrigin>, TNew>

	{
		static TNew Of(TOrigin origin)
			=> Of(property: ArtificialSource.Observed(origin));
		
		new static TNew Of(ObservedSource<TOrigin> property) 
			=> Of(ref property);
	}