using Mikita.Network.Messages;

namespace Mikita.Network.Pipeline;

public interface IMessageMiddleware: IMiddleware
	{
		MessageTypeId Id { get; }
	}