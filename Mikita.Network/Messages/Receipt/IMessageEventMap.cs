using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Messages.Receipt;

public interface IMessageEventMap<out T>
	{
		IMessageEvent<T> With
			(
				PeerKey senderKey
			);
	}