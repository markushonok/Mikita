using Mikita.Observation.Events;
using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Tables;

public sealed class EventSourceMap
	<
		TKey,
		TReaction
	>
	(
		IDictionary<TKey, IList<TReaction>> events
	)
	: IEventSourceMap<TKey, TReaction>
	where TReaction: notnull
	{
		IEvent<TReaction> IEventMap<TKey, TReaction>.Of
			(
				TKey key
			)
			=> Of(key);

		public IEventSource<TReaction> Of
			(
				TKey key
			)
			=> Event
				.Pattern<TReaction>
					(
						x => Add(key, x),
						x => Remove(key, x),
						x => Raise(key, x)
					);

		public void Raise
			(
				TKey key,
				Action<TReaction> pattern
			)
			{
				if (events.TryGetValue(key, out var reactions))
					reactions.ForEach(pattern);
			}

		private void Add
			(
				TKey key,
				TReaction reaction
			)
			{
				if (events.TryGetValue(key, out var subEvent))
					{
						subEvent.Add(reaction);
					}
				else events.Add(key, [reaction]);
			}

		private void Remove
			(
				TKey key,
				TReaction reaction
			)
			{
				var subEvent = events[key];

				if (!subEvent.Remove(reaction))
					throw ReactionRemovalException(key);

				if (subEvent is [] && !events.Remove(key))
					throw EventRemovalException(key);
			}

		private static Exception ReactionRemovalException(TKey key)
			=> new KeyNotFoundException
				(
					$"The reaction for the key '{key}' was not found"
					+ $" in the event source table, so it cannot be released."
				);

		private static Exception EventRemovalException(TKey key)
			=> new KeyNotFoundException
				(
					$"The event source with the key '{key}' was not found"
					+ $" in the event source table, so it cannot be removed."
				);
	}