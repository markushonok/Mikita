using Mikita.Identity;
using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Peers.Registries;

public static class PeerKeyPool
	{
		/// <summary>
		/// Creates an independent pool starting after peer key zero.
		/// </summary>
		public static IKeyPool<PeerKey> New
			=> KeyPool.NewWith
				(
					lastKey: new PeerKey(0),
					nextKey: x => new(unchecked(x.Number + 1)),
					unacquiredKeyMessage: key => $"Peer key '{key}' is not acquired."
				);
	}