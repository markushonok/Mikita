using System.Collections.Generic;

namespace Mikita.Observation.Tables;

public static class EventMapInstancing
	{
		extension
			<
				TKey,
				TReaction
			>
			(
				EventSourceMap<TKey, TReaction>
			)
			where TKey: notnull
			where TReaction: notnull
			{
				public static IEventSourceMap<TKey, TReaction> NewEmpty
					=> new EventSourceMap<TKey, TReaction>
						(
							new Dictionary<TKey, IList<TReaction>>()
						);
			}
	}