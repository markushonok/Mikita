using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using System.Collections.Generic;
using System.Net;

namespace Mikita.Network.Peers.Channels;

public static class PeerChannelRefCounting
	{
		extension(IPeerChannelRegistry registry)
			{
				public IPeerChannelRegistry WithReferenceCount
					=> new RefCountedRegistry
						(
							registry,
							endpoints: new Dictionary<PeerChannelId, IPEndPoint>(),
							ids: new Dictionary<IPEndPoint, PeerChannelId>
								(
									EndpointKeyComparer.Instance
								),
							references: new Dictionary<PeerChannelId, int>()
						);
			}
	}