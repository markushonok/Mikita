using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Peers.Registries;

public static class PeerRegistryPick
	{
		extension<T>(IPeerRegistry<T> registry)
			where T: class
			{
				public TemporalPeerKey<T> KeyFor(T peer)
					{
						var key = registry.ManualKeyFor(peer);
						return new TemporalPeerKey<T>(registry, key.Number);
					}
			}
	}