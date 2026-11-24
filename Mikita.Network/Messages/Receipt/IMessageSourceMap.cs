using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Messages.Receipt;

public interface IMessageSourceMap<T>
	: IMessageEventMap<T>
	{
		new IMessageSource<T> With
			(
				PeerKey partyKey
			);
	}