using Mikita.Network.Messages;

namespace Mikita.Network.Pipeline;

/// <summary>
/// Dispatches by the two-byte message type ID and passes the remaining payload
/// to its middleware. Unknown type IDs and truncated numbers go to typeMiss.
/// Registration changes must be serialized with dispatch.
/// </summary>
public interface IMessageTypeRouter
	: IMiddleware
	{
		void Add(IMessageMiddleware middleware);

		void Remove(MessageTypeId typeId);
	}