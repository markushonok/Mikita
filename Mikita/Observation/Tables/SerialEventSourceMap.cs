using Mikita.Observation.Events;
using System.Threading;

namespace Mikita.Observation.Tables;

/// <summary>
/// Serializes subscription changes and deliveries across a source map. All
/// access must pass through this instance; removing a hook waits for an
/// in-progress delivery. Direct calls to a retained hook are outside this
/// contract, and a delivery must not block on teardown that awaits it.
/// </summary>
public sealed class SerialEventSourceMap<TKey, TReaction>
	(
		IEventSourceMap<TKey, TReaction> source,
		Lock access
	)
	: IEventSourceMap<TKey, TReaction>
	{
		public IEventSource<TReaction> Of(TKey key)
			{
				lock (access)
					{
						return new SerialAccessEvent<TReaction>
							(
								source.Of(key),
								access
							);
					}
			}
	}