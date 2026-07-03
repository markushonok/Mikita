using Mikita.Observation.Events;

namespace Mikita.Observation.Tables;

public interface IEventMap
	<
		in TKey,
		in TReaction
	>
	{
		IEvent<TReaction> Of(TKey key);
	}