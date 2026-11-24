using Mikita.Network.Peers.Keys;
using Mikita.Observation.Events;
using Mikita.Structs.Enumerables;
using Mikita.Threading;
using System;
using System.Collections.Generic;

namespace Mikita.Network.Messages.Receipt;

public sealed class MessageSourceMap<T>
	(
		IDictionary<object, IList<Match<T>>> subEvents
	)
	: IMessageSourceMap<T>
	{
		IMessageEvent<T> IMessageEventMap<T>.With
			(
				PeerKey senderKey
			)
			=> With(senderKey);

		public IMessageSource<T> With
			(
				PeerKey senderKey
			)
			=> Event
				.Pattern<Match<T>>
					(
						x => Add(senderKey, x),
						x => Remove(senderKey, x),
						x => Raise(senderKey, x)
					)
				.Shorten;

		public void Raise
			(
				PeerKey senderKey,
				Action<Match<T>> pattern
			)
			=> subEvents[senderKey].ForEach(pattern);

		private void Add
			(
				PeerKey senderKey,
				Match<T> match
			)
			{
				if (subEvents.TryGetValue(senderKey, out var subEvent))
					{
					subEvent.Add(match);
					}
				else subEvents.Add(senderKey, [match]);
			}

		private void Remove
			(
				PeerKey senderKey,
				Match<T> match
			)
			{
				var subEvent = subEvents[senderKey];
			subEvent.Remove(match);

				if (subEvent is [])
					subEvents.Remove(senderKey);
			}
	}