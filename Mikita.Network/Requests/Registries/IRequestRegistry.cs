using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Requests.Registries;

public interface IRequestRegistry
	{
		RequestId Register(PeerKey recipient);

		PeerKey? RecipientKeyOf(RequestId request);

		void Release(RequestId request);
	}