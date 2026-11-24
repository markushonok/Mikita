using System.Threading;

namespace Mikita.Network.Peers.Channels;

public static class PeerChannelSerialAccess
	{
		extension(IPeerChannelRegistry registry)
			{
				public IPeerChannelRegistry WithSerialAccess
					=> new SerialAccessedRegistry(registry, new Lock());
			}
	}