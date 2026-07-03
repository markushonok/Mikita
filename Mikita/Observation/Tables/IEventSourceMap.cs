using Mikita.Observation.Events;

namespace Mikita.Observation.Tables;

public interface IEventSourceMap
	<
		in TKey,
		TReaction
	>
	: IEventMap<TKey, TReaction>
	{
		new IEventSource<TReaction> Of(TKey key);
	}