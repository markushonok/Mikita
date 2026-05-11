using Mikita.Observation.Events;
using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Tables;

public sealed class EventSourceTable
	<
		TKey,
		TReaction
	>
	(
		IDictionary<TKey, IList<TReaction>> events
	)
	: IEventSourceTable<TKey, TReaction>
	{
		IEvent<TReaction> IEventTable<TKey, TReaction>.Of
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
				subEvent.Remove(reaction);

				if (subEvent is [])
					events.Remove(key);
			}
	}