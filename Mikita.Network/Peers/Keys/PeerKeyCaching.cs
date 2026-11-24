namespace Mikita.Network.Peers.Keys;

public static class PeerKeyCaching
	{
		extension<TPeerKey>(TPeerKey key)
			where TPeerKey: IPeerKey
			{
				public PeerKey ValueSnapshot
					=> new(key.Number);
			}
	}