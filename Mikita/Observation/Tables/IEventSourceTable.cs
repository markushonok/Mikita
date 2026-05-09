using Mikita.Observation.Events;

namespace Mikita.Observation.Tables;

public interface IEventSourceTable
	<
		in TKey,
		TReaction
	>
	: IEventTable<TKey, TReaction>
	{
		new IEventSource<TReaction> Of(TKey key);
	}