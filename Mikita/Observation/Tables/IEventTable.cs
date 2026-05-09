using Mikita.Observation.Events;

namespace Mikita.Observation.Tables;

public interface IEventTable
	<
		in TKey,
		in TReaction
	>
	{
		IEvent<TReaction> Of(TKey key);
	}