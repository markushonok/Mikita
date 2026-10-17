using Mikita.Observation.Events;

namespace Mikita.Observation.Tables;

public interface IEventSourceMap
	<
		in TKey,
		TReaction
	>
	: IEventMap<TKey, TReaction>
	{
		IEvent<TReaction> IEventMap<TKey, TReaction>.Of(TKey key)
			=> Of(key);

		new IEventSource<TReaction> Of(TKey key);
	}