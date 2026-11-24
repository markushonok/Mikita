using Mikita.Observation.Events;
using Mikita.Threading;

namespace Mikita.Network.Messages.Receipt;

public static class MessageEventInstancing
	{
		extension<T>(MessageEvent<T>)
			{
				public static IMessageEvent<T> NewEmpty
					=> Event<Match<T>>.NewEmpty.Shorten;
			}

		extension<T>(IEventSource<Match<T>> @event)
			{
				public IMessageSource<T> Shorten
					=> new MessageEvent<T>(@event);
			}
	}