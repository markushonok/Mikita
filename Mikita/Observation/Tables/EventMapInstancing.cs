using System.Collections.Generic;
using System.Threading;

namespace Mikita.Observation.Tables;

public static class EventMapInstancing
	{
		extension<TKey, TReaction>(IEventSourceMap<TKey, TReaction> source)
			{
				/// <summary>Serializes subscription changes and event delivery.</summary>
				public IEventSourceMap<TKey, TReaction> WithSerialAccess
					=> new SerialEventSourceMap<TKey, TReaction>(source, new Lock());
			}

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