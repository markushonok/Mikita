using Mikita.Network.Messages;

namespace Mikita.Network.Pipeline;

public static class MessageMiddlewareInstancing
	{
		extension(IMiddleware middleware)
			{
				public IMessageMiddleware With
					(
						MessageTypeId typeId
					)
					=> new MessageMiddleware(middleware, typeId);
			}
	}