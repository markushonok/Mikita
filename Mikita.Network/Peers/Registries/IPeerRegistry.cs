using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Peers.Registries;

public interface IPeerRegistry<T>
	where T: class
	{
		PeerKey ManualKeyFor(T peer);

		T? PeerWith(PeerKey key);

		void Release(PeerKey key);
	}