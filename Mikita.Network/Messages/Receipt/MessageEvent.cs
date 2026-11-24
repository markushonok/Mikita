using Mikita.Observation.Events;
using Mikita.Threading;
using System;

namespace Mikita.Network.Messages.Receipt;

public sealed class MessageEvent<T>
	(
		IEventSource<Match<T>> @event
	)
	: IMessageSource<T>
	{
		public void Add(Match<T> match)
			=> @event.Add(match);

		public void Remove(Match<T> match)
			=> @event.Remove(match);

		public void Raise(Action<Match<T>> pattern)
			=> @event.Raise(pattern);
	}

public static class MessageEvent;