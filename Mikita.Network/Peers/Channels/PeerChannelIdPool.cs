using Mikita.Identity;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Issues unique peer channel ids,
/// reusing numbers released when
/// channels are disposed.
/// </summary>
public static class PeerChannelIdPool
	{
		public static IKeyPool<PeerChannelId> New
			=> KeyPool.NewWith
				(
					lastKey: new PeerChannelId(0),
					nextKey: x => new(unchecked(x.Number + 1)),
					key => $"Channel id '{key}' is not acquired."
				);
	}