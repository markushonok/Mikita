using System.Collections.Generic;

namespace Mikita.Observation.Tables;

public static class EventTableInstancing
	{
		extension
			<
				TKey,
				TReaction
			>
			(
				EventSourceTable<TKey, TReaction>
			)
			where TKey: notnull
			{
				public static IEventSourceTable<TKey, TReaction> NewEmpty
					=> new EventSourceTable<TKey, TReaction>
						(
							new Dictionary<TKey, IList<TReaction>>()
						);
			}
	}